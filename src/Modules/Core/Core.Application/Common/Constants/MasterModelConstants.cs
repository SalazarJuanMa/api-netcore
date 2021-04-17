// ***********************************************************************
// Assembly         : Application
// Author           : jrosas
// Created          : 09-09-2020
//
// Last Modified By : jrosas
// Last Modified On : 09-09-2020
// ***********************************************************************
// <copyright file="MasterModelConstants.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace Core.Application.Common.Constants
{
    /// <summary>
    /// Class MasterModelConstants Constants.
    /// </summary>
    public static class MasterModelConstants
    {
        public const string APP = "mastermodelapp";
        public const string CONTENT_TYPE_JSON = "application/json";
        public const string GET = "GET";
        public const string JWT_AUDIENCE = "*.app.yaengage.com";
        public const string NO = "NO";
        public const string ROUTE_CONTROLLER = "/API/V1/";
        public const string ROUTE_CONTROLLER_CHANGE = "/API/V1/CHANGE";
        public const string SECURE_TOKEN = "SECURETOKEN";
        public const string SUCCESS = "Success";
        public const string TYPE = "Type";

        /// <summary>
        /// Class EnviromentVarName.
        /// </summary>
        public static class EnviromentVarName
        {
            public const string EXPIRATION_IN_MINUTES = "EXPIRATION_IN_MINUTES";
            public const string GFS_CONNSTRING = "GFS_CONNSTRING";
            public const string JWT_ISSUER = "JWT_ISSUER";
            public const string JWT_SECRET = "JWT_SECRET";
        }


        /// <summary>
        /// Class Header.
        /// </summary>
        public static class Header
        {
            public const string OPTION = "mastermodelOption";
            public const string SECURE_TOKEN = "Authorization";
            public const string TOKEN_OR_OPTION_REQUIRED = "User Id, Token and Option are required parameters.";
            public const string USERID = "userId";
        }

        /// <summary>
        /// Class Menu.
        /// </summary>
        public static class Menu
        {
        }

        /// <summary>
        /// Class Navigation.
        /// </summary>
        public static class Navigation
        {
        }

        /// <summary>
        /// Class Message.
        /// </summary>
        public static class Message
        {
        }

        public static class Action
        {
            public const string ACTION_CREATE = "CREATE";
            public const string ACTION_UPDATE = "UPDATE";
            public const string ACTION_DELETE = "DELETE";
            public const string ACTION_IMPORT = "IMPORT";
            public static Dictionary<string, string> RESPONSE_MSG = new Dictionary<string, string>
            {
                { ACTION_CREATE, "The record has been added successfully." },
                { ACTION_UPDATE, "The record has been updated successfully." },
                { ACTION_DELETE, "The record has been deleted successfully." },
                { ACTION_IMPORT, "The files has been imported successfully." }
            };
        }
    }
}
