using System;

namespace SDG.Provider.Services.Web
{
	// Token: 0x02000360 RID: 864
	public interface IWebService : IService
	{
		// Token: 0x060017BF RID: 6079
		IWebRequestHandle createRequest(string url, ERequestType requestType, WebRequestReadyCallback webRequestReadyCallback);

		// Token: 0x060017C0 RID: 6080
		void updateRequest(IWebRequestHandle webRequestHandle, string key, string value);

		// Token: 0x060017C1 RID: 6081
		void submitRequest(IWebRequestHandle webRequestHandle);

		// Token: 0x060017C2 RID: 6082
		void releaseRequest(IWebRequestHandle webRequestHandle);

		// Token: 0x060017C3 RID: 6083
		uint getResponseBodySize(IWebRequestHandle webRequestHandle);

		// Token: 0x060017C4 RID: 6084
		void getResponseBodyData(IWebRequestHandle webRequestHandle, byte[] data, uint size);
	}
}
