using System;
using System.Collections.Generic;
using SDG.Provider.Services;
using SDG.Provider.Services.Web;
using Steamworks;

namespace SDG.SteamworksProvider.Services.Web
{
	// Token: 0x0200037D RID: 893
	public class SteamworksWebService : Service, IWebService, IService
	{
		// Token: 0x06001879 RID: 6265 RVA: 0x00089824 File Offset: 0x00087C24
		public SteamworksWebService()
		{
			this.steamworksWebRequestHandles = new List<SteamworksWebRequestHandle>();
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x00089838 File Offset: 0x00087C38
		private SteamworksWebRequestHandle findSteamworksWebRequestHandle(IWebRequestHandle webRequestHandle)
		{
			return this.steamworksWebRequestHandles.Find((SteamworksWebRequestHandle handle) => handle == webRequestHandle);
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x0008986C File Offset: 0x00087C6C
		public IWebRequestHandle createRequest(string url, ERequestType requestType, WebRequestReadyCallback webRequestReadyCallback)
		{
			HTTPRequestHandle newHTTPRequestHandle = SteamHTTP.CreateHTTPRequest((requestType != ERequestType.GET) ? EHTTPMethod.k_EHTTPMethodPOST : EHTTPMethod.k_EHTTPMethodGET, url);
			SteamworksWebRequestHandle steamworksWebRequestHandle = new SteamworksWebRequestHandle(newHTTPRequestHandle, webRequestReadyCallback);
			this.steamworksWebRequestHandles.Add(steamworksWebRequestHandle);
			return steamworksWebRequestHandle;
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x000898A4 File Offset: 0x00087CA4
		public void updateRequest(IWebRequestHandle webRequestHandle, string key, string value)
		{
			SteamworksWebRequestHandle steamworksWebRequestHandle = this.findSteamworksWebRequestHandle(webRequestHandle);
			SteamHTTP.SetHTTPRequestGetOrPostParameter(steamworksWebRequestHandle.getHTTPRequestHandle(), key, value);
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x000898C8 File Offset: 0x00087CC8
		public void submitRequest(IWebRequestHandle webRequestHandle)
		{
			SteamworksWebRequestHandle steamworksWebRequestHandle = this.findSteamworksWebRequestHandle(webRequestHandle);
			SteamAPICall_t hAPICall;
			SteamHTTP.SendHTTPRequest(steamworksWebRequestHandle.getHTTPRequestHandle(), out hAPICall);
			CallResult<HTTPRequestCompleted_t> callResult = CallResult<HTTPRequestCompleted_t>.Create(new CallResult<HTTPRequestCompleted_t>.APIDispatchDelegate(this.onHTTPRequestCompleted));
			callResult.Set(hAPICall, null);
			steamworksWebRequestHandle.setHTTPRequestCompletedCallResult(callResult);
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x0008990C File Offset: 0x00087D0C
		public void releaseRequest(IWebRequestHandle webRequestHandle)
		{
			SteamworksWebRequestHandle steamworksWebRequestHandle = this.findSteamworksWebRequestHandle(webRequestHandle);
			this.steamworksWebRequestHandles.Remove(steamworksWebRequestHandle);
			SteamHTTP.ReleaseHTTPRequest(steamworksWebRequestHandle.getHTTPRequestHandle());
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x0008993C File Offset: 0x00087D3C
		public uint getResponseBodySize(IWebRequestHandle webRequestHandle)
		{
			SteamworksWebRequestHandle steamworksWebRequestHandle = this.findSteamworksWebRequestHandle(webRequestHandle);
			uint result;
			SteamHTTP.GetHTTPResponseBodySize(steamworksWebRequestHandle.getHTTPRequestHandle(), out result);
			return result;
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x00089960 File Offset: 0x00087D60
		public void getResponseBodyData(IWebRequestHandle webRequestHandle, byte[] data, uint size)
		{
			SteamworksWebRequestHandle steamworksWebRequestHandle = this.findSteamworksWebRequestHandle(webRequestHandle);
			SteamHTTP.GetHTTPResponseBodyData(steamworksWebRequestHandle.getHTTPRequestHandle(), data, size);
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x00089984 File Offset: 0x00087D84
		private void onHTTPRequestCompleted(HTTPRequestCompleted_t callback, bool ioFailure)
		{
			SteamworksWebRequestHandle steamworksWebRequestHandle = this.steamworksWebRequestHandles.Find((SteamworksWebRequestHandle handle) => handle.getHTTPRequestHandle() == callback.m_hRequest);
			steamworksWebRequestHandle.triggerWebRequestReadyCallback();
		}

		// Token: 0x04000CFB RID: 3323
		private List<SteamworksWebRequestHandle> steamworksWebRequestHandles;
	}
}
