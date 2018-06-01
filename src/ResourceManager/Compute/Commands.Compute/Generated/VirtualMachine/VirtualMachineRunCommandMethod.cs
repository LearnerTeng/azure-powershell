//
// Copyright (c) Microsoft and contributors.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//
// See the License for the specific language governing permissions and
// limitations under the License.
//

// Warning: This code was generated by a tool.
//
// Changes to this file may cause incorrect behavior and will be lost if the
// code is regenerated.

using Microsoft.Azure.Commands.Compute.Automation.Models;
using Microsoft.Azure.Management.Compute;
using Microsoft.Azure.Management.Compute.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Compute.Automation
{
    public partial class InvokeAzureComputeMethodCmdlet : ComputeAutomationBaseCmdlet
    {
        protected object CreateVirtualMachineRunCommandDynamicParameters()
        {
            dynamicParameters = new RuntimeDefinedParameterDictionary();
            var pResourceGroupName = new RuntimeDefinedParameter();
            pResourceGroupName.Name = "ResourceGroupName";
            pResourceGroupName.ParameterType = typeof(string);
            pResourceGroupName.Attributes.Add(new ParameterAttribute
            {
                ParameterSetName = "InvokeByDynamicParameters",
                Position = 1,
                Mandatory = true
            });
            pResourceGroupName.Attributes.Add(new AllowNullAttribute());
            dynamicParameters.Add("ResourceGroupName", pResourceGroupName);

            var pVMName = new RuntimeDefinedParameter();
            pVMName.Name = "VMName";
            pVMName.ParameterType = typeof(string);
            pVMName.Attributes.Add(new ParameterAttribute
            {
                ParameterSetName = "InvokeByDynamicParameters",
                Position = 2,
                Mandatory = true
            });
            pVMName.Attributes.Add(new AllowNullAttribute());
            dynamicParameters.Add("VMName", pVMName);

            var pParameters = new RuntimeDefinedParameter();
            pParameters.Name = "RunCommandInput";
            pParameters.ParameterType = typeof(RunCommandInput);
            pParameters.Attributes.Add(new ParameterAttribute
            {
                ParameterSetName = "InvokeByDynamicParameters",
                Position = 3,
                Mandatory = true
            });
            pParameters.Attributes.Add(new AllowNullAttribute());
            dynamicParameters.Add("RunCommandInput", pParameters);

            var pArgumentList = new RuntimeDefinedParameter();
            pArgumentList.Name = "ArgumentList";
            pArgumentList.ParameterType = typeof(object[]);
            pArgumentList.Attributes.Add(new ParameterAttribute
            {
                ParameterSetName = "InvokeByStaticParameters",
                Position = 4,
                Mandatory = true
            });
            pArgumentList.Attributes.Add(new AllowNullAttribute());
            dynamicParameters.Add("ArgumentList", pArgumentList);

            return dynamicParameters;
        }

        protected void ExecuteVirtualMachineRunCommandMethod(object[] invokeMethodInputParameters)
        {
            string resourceGroupName = (string)ParseParameter(invokeMethodInputParameters[0]);
            string vmName = (string)ParseParameter(invokeMethodInputParameters[1]);
            RunCommandInput parameters = (RunCommandInput)ParseParameter(invokeMethodInputParameters[2]);

            var result = VirtualMachinesClient.RunCommand(resourceGroupName, vmName, parameters);
            WriteObject(result);
        }
    }

    public partial class NewAzureComputeArgumentListCmdlet : ComputeAutomationBaseCmdlet
    {
        protected PSArgument[] CreateVirtualMachineRunCommandParameters()
        {
            string resourceGroupName = string.Empty;
            string vmName = string.Empty;
            RunCommandInput parameters = new RunCommandInput();

            return ConvertFromObjectsToArguments(
                 new string[] { "ResourceGroupName", "VMName", "Parameters" },
                 new object[] { resourceGroupName, vmName, parameters });
        }
    }

    [Cmdlet(VerbsLifecycle.Invoke, "AzureRmVMRunCommand", DefaultParameterSetName = "DefaultParameter", SupportsShouldProcess = true)]
    [OutputType(typeof(PSRunCommandResult))]
    public partial class InvokeAzureRmVMRunCommand : ComputeAutomationBaseCmdlet
    {
        public override void ExecuteCmdlet()
        {
            ExecuteClientAction(() =>
            {
                if (ShouldProcess(this.VMName, VerbsLifecycle.Invoke))
                {
                    string resourceGroupName = this.ResourceGroupName;
                    string vmName = this.VMName;
                    RunCommandInput parameters = new RunCommandInput();
                    parameters.CommandId = this.CommandId;
                    if (this.ScriptPath != null)
                    {
                        parameters.Script = new List<string>();
                        PathIntrinsics currentPath = SessionState.Path;
                        var filePath = new System.IO.FileInfo(currentPath.GetUnresolvedProviderPathFromPSPath(this.ScriptPath));
                        string fileContent = Commands.Common.Authentication.Abstractions.FileUtilities.DataStore.ReadFileAsText(filePath.FullName);
                        parameters.Script = fileContent.Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    if (this.Parameter != null)
                    {
                        var vParameter = new List<RunCommandInputParameter>();
                        foreach (var key in this.Parameter.Keys)
                        {
                            RunCommandInputParameter p = new RunCommandInputParameter();
                            p.Name = key.ToString();
                            p.Value = this.Parameter[key].ToString();
                            vParameter.Add(p);
                        }
                        parameters.Parameters = vParameter;
                    }
                    if (this.VM != null)
                    {
                        vmName = VM.Name;
                        resourceGroupName = VM.ResourceGroupName;
                    }

                    var result = VirtualMachinesClient.RunCommand(resourceGroupName, vmName, parameters);
                    var psObject = new PSRunCommandResult();
                    ComputeAutomationAutoMapperProfile.Mapper.Map<RunCommandResult, PSRunCommandResult>(result, psObject);
                    WriteObject(psObject);
                }
            });
        }

        [Parameter(
            ParameterSetName = "DefaultParameter",
            Position = 1,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true)]
        [ResourceManager.Common.ArgumentCompleters.ResourceGroupCompleter()]
        public string ResourceGroupName { get; set; }

        [Parameter(
            ParameterSetName = "DefaultParameter",
            Position = 2,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true)]
        [Alias("Name")]
        public string VMName { get; set; }

        [Parameter(
            Mandatory = true)]
        [AllowNull]
        public string CommandId { get; set; }

        [Parameter(
            Mandatory = false)]
        [AllowNull]
        public string ScriptPath { get; set; }

        [Parameter(
            Mandatory = false)]
        [AllowNull]
        public Hashtable Parameter { get; set; }

        [Alias("VMProfile")]
        [Parameter(
            ParameterSetName = "VMParameter",
            Position = 0,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public Compute.Models.PSVirtualMachine VM { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Run cmdlet in the background")]
        public SwitchParameter AsJob { get; set; }
    }
}
