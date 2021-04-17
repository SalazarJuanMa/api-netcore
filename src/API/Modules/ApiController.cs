// ***********************************************************************
// Assembly         : APP
// Author           : jrosas
// Created          : 08-13-2020
//
// Last Modified By : jrosas
// Last Modified On : 11-03-2020
// ***********************************************************************
// <copyright file="ApiController.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mime;

namespace APP.Modules
{
    /// <summary>
    /// Class ApiController.
    /// Implements the <see cref="ControllerBase" />
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [ApiController]
    [Route("[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiExplorerSettings(IgnoreApi = false)] // or [ApiController]
    public partial class ApiController : ControllerBase
    {

        /// <summary>
        /// The mediator
        /// </summary>
        protected IMediator _mediator;

        /// <summary>
        /// Gets the mediator.
        /// </summary>
        /// <returns>The mediator.</returns>
        protected IMediator GetMediator()
        {
            return _mediator ?? HttpContext.RequestServices.GetService<IMediator>();
        }
    }
}
