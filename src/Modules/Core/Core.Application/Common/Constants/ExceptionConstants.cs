// ***********************************************************************
// Assembly         : Application
// Author           : jrosas
// Created          : 09-09-2020
//
// Last Modified By : jrosas
// Last Modified On : 09-09-2020
// ***********************************************************************
// <copyright file="ExceptionConstants.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Core.Application.Common.Constants
{
    /// <summary>
    /// Class Exception Constants.
    /// </summary>
    public static class ExceptionConstants
    {
        /// <summary>
        /// Class Message.
        /// </summary>
        public static class Message
        {
            public const string DATABASE_GET_EXCEPTION = "Error while getting records from the database.";
            public const string HANDLE_EXCEPTION = "App Setup Request: Unhandled Exception for Request ";
            public const string REQUEST_EXCEPTION = "Validate that the information sent in the request body.";
            public const string SECURE_TOKEN_EXCEPTION = "Something is wrong with the username or password, please try again.";
        }
    }
}
