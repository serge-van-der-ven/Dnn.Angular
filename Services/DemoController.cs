// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DemoController.cs" company="XCESS expertise center bv">
//   Copyright (c) 2015 XCESS expertise center bv
//   
//   The software is owned by XCESS and is protected by 
//   the Dutch copyright laws and international treaty provisions.
//   You are allowed to make copies of the software solely for backup or archival purposes.
//   You may not lease, rent, export or sublicense the software.
//   You may not reverse engineer, decompile, disassemble or create derivative works from the software.
//   
//   Owned by XCESS expertise center b.v., Storkstraat 19, 3833 LB Leusden, The Netherlands
//   T. +31-33-4335151, E. info@xcess.nl, I. http://www.xcess.nl
// </copyright>
// <summary>
//   
// </summary>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Web.Http;
using Dnn.Angular.Demo.Services.ViewModels;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Web.Api;

namespace Dnn.Angular.Demo.Services
{
    // [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
    public class DemoController : DnnApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<ItemDto> GetList()
        {
            var items = new List<ItemDto>();
            var moduleId = this.ActiveModule?.ModuleID ?? Null.NullInteger;
            var random = new Random();

            for (var index = 0; index < random.Next(5, 10); index++)
            {
                items.Add(new ItemDto()
                          {
                              Id = index,
                              Name = $"Item #{index} for module {moduleId}",
                              PhoneNumber = $"06-123-{index}"
                          });
            }

            return items;
        }
    }
}