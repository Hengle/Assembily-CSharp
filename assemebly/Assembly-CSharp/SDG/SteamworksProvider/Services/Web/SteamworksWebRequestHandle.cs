using System;
using SDG.Provider.Services.Web;
using Steamworks;

namespace SDG.SteamworksProvider.Services.Web
{
	// Token: 0x0200037C RID: 892
	public class SteamworksWebRequestHandle : IWebRequestHandle
	{
		// Token: 0x06001874 RID: 6260 RVA: 0x000897DB File Offset: 0x00087BDB
		public SteamworksWebRequestHandle(HTTPRequestHandle newHTTPRequestHandle, WebRequestReadyCallback newWebRequestReadyCallback)
		{
			this.setHTTPRequestHandle(newHTTPRequestHandle);
			this.webRequestReadyCallback = newWebRequestReadyCallback;
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x000897F1 File Offset: 0x00087BF1
		public HTTPRequestHandle getHTTPRequestHandle()
		{
			return this.httpRequestHandle;
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x000897F9 File Offset: 0x00087BF9
		protected void setHTTPRequestHandle(HTTPRequestHandle newHTTPRequestHandle)
		{
			this.httpRequestHandle = newHTTPRequestHandle;
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x00089802 File Offset: 0x00087C02
		public void setHTTPRequestCompletedCallResult(CallResult<HTTPRequestCompleted_t> newHTTPRequestCompletedCallResult)
		{
			this.httpRequestCompletedCallResult = newHTTPRequestCompletedCallResult;
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x0008980B File Offset: 0x00087C0B
		public void triggerWebRequestReadyCallback()
		{
			if (this.webRequestReadyCallback != null)
			{
				this.webRequestReadyCallback(this);
			}
		}

		// Token: 0x04000CF8 RID: 3320
		private HTTPRequestHandle httpRequestHandle;

		// Token: 0x04000CF9 RID: 3321
		private CallResult<HTTPRequestCompleted_t> httpRequestCompletedCallResult;

		// Token: 0x04000CFA RID: 3322
		private WebRequestReadyCallback webRequestReadyCallback;
	}
}
