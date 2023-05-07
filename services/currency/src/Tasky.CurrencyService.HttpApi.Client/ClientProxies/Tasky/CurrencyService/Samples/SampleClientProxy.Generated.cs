// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasky.CurrencyService.Samples;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Client.ClientProxying;
using Volo.Abp.Http.Modeling;

// ReSharper disable once CheckNamespace
namespace Tasky.CurrencyService.Samples;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(ISampleAppService), typeof(SampleClientProxy))]
public partial class SampleClientProxy : ClientProxyBase<ISampleAppService>, ISampleAppService
{
    public virtual async Task<SampleDto> GetAsync()
    {
        return await RequestAsync<SampleDto>(nameof(GetAsync));
    }

    public virtual async Task<SampleDto> GetAuthorizedAsync()
    {
        return await RequestAsync<SampleDto>(nameof(GetAuthorizedAsync));
    }
}
