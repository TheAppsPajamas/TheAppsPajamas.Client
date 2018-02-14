﻿using System;
namespace Build.Client.Constants
{
    public static class MetadataType
    {
        /// <summary>
        /// The idiom for/from the iOS Contents.json file.
        /// </summary>
        public const string Idiom = "idiom";

        /// <summary>
        /// The idiom2 from thepajamaapp fieldtypes, will become idiom in Contents.json files. This is for files that are reused in the Contents.json file.
        /// </summary>
        public const string Idiom2 = "idiom2";

        /// <summary>
        /// The size for/from the iOS Contents.json file.
        /// </summary>
        public const string Size = "size";

        /// <summary>
        /// The scale for/from the iOS Contents.json file.
        /// </summary>
        public const string Scale = "scale";

        /// <summary>
        /// The iOS subtype for/from Contents.json
        /// </summary>
        public const string Subtype = "subtype";

        /// <summary>
        /// The extent for/from Contents.json.
        /// </summary>
        public const string Extent = "extent";
            
        /// <summary>
        /// The orientation for/from Contents.json.
        /// </summary>
        public const string Orientation = "orientation";

        /// <summary>
        /// The MinimumSystemVersion for/from Contents.json.
        /// </summary>
        public const string MinimumSystemVersion = "minimum-system-version";

        /// <summary>
        /// The filename for/from the Contents.json file
        /// </summary>
        public const string FileName = "filename";

        /// <summary>
        /// The field description - mostly used for logging.
        /// </summary>
        public const string FieldDescription = "FieldDescription";

        /// <summary>
        /// If the field is actually disabled.
        /// </summary>
        public const string Disabled = "Disabled";

        /// <summary>
        /// The path of the file to be deleted from the project.
        /// </summary>
        public const string DeletePath = "DeletePath";

        /// <summary>
        /// The relative path (without file name) of the appspajamas media file.
        /// </summary>
        public const string Path = "Path";

        /// <summary>
        /// The include path for files that are going to be added to the project.
        /// </summary>
        public const string IncludePath = "IncludePath";


        public const string MediaFileId = "MediaFileId";

        /// <summary>
        /// The name of the media file, including _id from theappspajama server, without extension, and without path.
        /// </summary>
        public const string MediaName = "MediaName";

        /// <summary>
        /// The name of the file as it will be stored in the project for final use, i.e. as part of an asset catalogue.
        /// </summary>
        public const string LogicalName = "LogicalName";

        /// <summary>
        /// The name of the iOS asset catalogue this file goes into.
        /// </summary>
        public const string ContentsFileName = "ContentsFileName";

        /// <summary>
        /// The folder that the droid media will be stored in.
        /// </summary>
        public const string Folder = "folder";

        /// <summary>
        /// The type of the MSB Build item with ItemGroup, e.g. ImageAsset or AndroidResource.
        /// </summary>
        public const string MSBuildItemType = "MSBuildItemType";

        /// <summary>
        /// The client dto field value (calculated value)
        /// </summary>
        public const string Value = "Value";

        /// <summary>
        /// The asset catalogue set name, i.e. AppIcon.appicon, Launch, Image etc
        /// </summary>
        public const string CatalogueSetName = "CatalogueSetName";

        /// <summary>
        /// The catalogue packaging (for landscape / portrait image sets, id)
        /// </summary>
        public const string CataloguePackagingFieldId = "CataloguePackagingFieldId";
    }
}
