using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000474 RID: 1140
	public class CommandWeather : Command
	{
		// Token: 0x06001E3F RID: 7743 RVA: 0x000A5948 File Offset: 0x000A3D48
		public CommandWeather(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("WeatherCommandText");
			this._info = this.localization.format("WeatherInfoText");
			this._help = this.localization.format("WeatherHelpText");
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x000A59A4 File Offset: 0x000A3DA4
		protected override void execute(CSteamID executorID, string parameter)
		{
			string text = parameter.ToLower();
			if (text == this.localization.format("WeatherNone").ToLower())
			{
				if (LightingManager.hasRain && LevelLighting.rainyness == ELightingRain.DRIZZLE)
				{
					LightingManager.rainDuration = 0u;
				}
				if (LightingManager.hasSnow && LevelLighting.snowyness == ELightingSnow.BLIZZARD)
				{
					LightingManager.snowDuration = 0u;
				}
			}
			else if (text == this.localization.format("WeatherStorm").ToLower())
			{
				if (!LightingManager.hasRain)
				{
					return;
				}
				if (LevelLighting.rainyness == ELightingRain.NONE)
				{
					LightingManager.rainFrequency = 0u;
				}
				else if (LevelLighting.rainyness == ELightingRain.DRIZZLE)
				{
					LightingManager.rainDuration = 0u;
				}
			}
			else
			{
				if (!(text == this.localization.format("WeatherBlizzard").ToLower()))
				{
					CommandWindow.LogError(this.localization.format("NoWeatherErrorText", new object[]
					{
						text
					}));
					return;
				}
				if (!LightingManager.hasSnow)
				{
					return;
				}
				if (LevelLighting.snowyness == ELightingSnow.NONE)
				{
					LightingManager.snowFrequency = 0u;
				}
				else if (LevelLighting.snowyness == ELightingSnow.BLIZZARD)
				{
					LightingManager.snowDuration = 0u;
				}
			}
			CommandWindow.Log(this.localization.format("WeatherText", new object[]
			{
				text
			}));
		}
	}
}
