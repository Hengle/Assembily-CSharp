using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000441 RID: 1089
	public class CommandCamera : Command
	{
		// Token: 0x06001DD4 RID: 7636 RVA: 0x000A138C File Offset: 0x0009F78C
		public CommandCamera(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("CameraCommandText");
			this._info = this.localization.format("CameraInfoText");
			this._help = this.localization.format("CameraHelpText");
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x000A13E8 File Offset: 0x0009F7E8
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (!Dedicator.isDedicated)
			{
				return;
			}
			string text = parameter.ToLower();
			ECameraMode cameraMode;
			if (text == this.localization.format("CameraFirst").ToLower())
			{
				cameraMode = ECameraMode.FIRST;
			}
			else if (text == this.localization.format("CameraThird").ToLower())
			{
				cameraMode = ECameraMode.THIRD;
			}
			else if (text == this.localization.format("CameraBoth").ToLower())
			{
				cameraMode = ECameraMode.BOTH;
			}
			else
			{
				if (!(text == this.localization.format("CameraVehicle").ToLower()))
				{
					CommandWindow.LogError(this.localization.format("NoCameraErrorText", new object[]
					{
						text
					}));
					return;
				}
				cameraMode = ECameraMode.VEHICLE;
			}
			if (Provider.isServer)
			{
				CommandWindow.LogError(this.localization.format("RunningErrorText"));
				return;
			}
			Provider.cameraMode = cameraMode;
			CommandWindow.Log(this.localization.format("CameraText", new object[]
			{
				text
			}));
		}
	}
}
