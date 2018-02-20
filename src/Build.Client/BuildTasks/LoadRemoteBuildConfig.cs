﻿using System;
using System.IO;
using System.Net;
using Build.Client.Models;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Newtonsoft.Json;
using Build.Client.Extensions;
using System.Linq;
using Build.Client.Constants;
using System.Text;
using Build.Shared.Types;
using System.Collections.Generic;

namespace Build.Client.BuildTasks
{
    public class LoadRemoteBuildConfig : BaseLoadTask
    {
        [Output]
        public ITaskItem Token { get; set; }

        [Output]
        public ITaskItem BuildAppId { get; set; }

        public LoadRemoteBuildConfig()
        {
            _taskName = "LoadRemoteBuildConfig";
        }

        public override bool Execute()
        {
            var baseResult = base.Execute();
            if (baseResult == false){
                return false;
            }

            TapResourceDir = this.GetTapResourcesDir();
            if (String.IsNullOrEmpty(TapResourceDir))
            {
                Log.LogError($"{Consts.TapResourcesDir} folder not found, exiting");
                return false;
            }


            var securityConfig = this.GetSecurityConfig();

            if (securityConfig == null)
            {
                Log.LogError($"{Consts.TapSecurityConfig} file not set, please see solution root and complete");
                return false;
            }

            BuildAppId = new TaskItem(_tapResourcesConfig.TapAppId.ToString());

            Token = this.Login(securityConfig);
            if (Token == null){
                Log.LogError("Authentication failure");
                return false;
            }

            var unmodifedProjectName = ProjectName.Replace(Consts.ModifiedProjectNameExtra, String.Empty);
            var url = String.Concat(Consts.UrlBase, Consts.ClientEndpoint, "?", "appId=", _tapResourcesConfig.TapAppId, "&projectName=", unmodifedProjectName, "&buildConfiguration=", BuildConfiguration );

            Log.LogMessage("Loading remote build config from '{0}'", url);

            string jsonClientConfig = null;
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.SetWebClientHeaders(Token);

                    jsonClientConfig = client.DownloadString(url);
                    Log.LogMessage("Successfully loaded remote build config from '{0}', recieved '{1}'", url, jsonClientConfig.Length);
                    LogDebug("Json data recieved\n{0}", jsonClientConfig);
                    //write to file
                    //deserialise now (extension method)
                    //return object
                }
            }
            catch (WebException ex){
                var response = ex.Response as HttpWebResponse;
                if (response == null)
                {
                    LogDebug("Webexception not ususal status code encountered, fatal, exiting");
                    Log.LogErrorFromException(ex);
                    return false;
                }

                if (response.StatusCode == HttpStatusCode.NotFound){
                    Log.LogWarning($"Tap server responded with message '{response.StatusDescription}', TheAppsPajamams cannot continue, exiting gracefully, build will continue");
                    TapShouldContinue = bool.FalseString;
                    return true;
                }

            }
            catch (Exception ex)
            {
                Log.LogErrorFromException(ex);
                return false;
            }


            ClientConfigDto clientConfigDto = this.GetClientConfig(jsonClientConfig);
            if (clientConfigDto == null)
                return false;

            //this is not quite identical in base

            var projectsConfig = this.GetProjectsConfig();

            var projectConfig = projectsConfig.Projects.FirstOrDefault(x => x.BuildConfiguration == BuildConfiguration);

            if (projectConfig == null){
                projectConfig = new ProjectConfig();
                projectsConfig.Projects.Add(projectConfig);
            }

            projectConfig.BuildConfiguration = BuildConfiguration;
            projectConfig.ClientConfig = clientConfigDto;
            if (!this.SaveProjects(projectsConfig))
                return false;

            //this is identical in base

            AssetCatalogueName = this.GetAssetCatalogueName(projectConfig.ClientConfig, TargetFrameworkIdentifier);

            AppIconOutput = this.GetMediaOutput(projectConfig.ClientConfig.AppIcon.Fields, AssetCatalogueName, projectConfig.ClientConfig);

            SplashOutput = this.GetMediaOutput(projectConfig.ClientConfig.Splash.Fields, AssetCatalogueName, projectConfig.ClientConfig);

            PackagingOutput = this.GetFieldTypeOutput(projectConfig.ClientConfig.Packaging.Fields);


            return true;
        }
    }
}
