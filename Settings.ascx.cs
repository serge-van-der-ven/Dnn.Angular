// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Settings.ascx.cs" company="XCESS expertise center bv">
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
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;

namespace Dnn.Angular.Demo
{
    public partial class Settings : ModuleSettingsBase
    {
        #region Base Method Implementations

        public override void LoadSettings()
        {
            try
            {
                if (!this.Page.IsPostBack)
                {}
            }
            catch (Exception exception)
            {
                Exceptions.ProcessModuleLoadException(this, exception);
            }
        }

        public override void UpdateSettings()
        {
            try
            {}
            catch (Exception exception)
            {
                Exceptions.ProcessModuleLoadException(this, exception);
            }
        }

        #endregion
    }
}