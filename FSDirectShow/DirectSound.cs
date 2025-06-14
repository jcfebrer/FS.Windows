using System;
using System.Collections.Generic;
using FSException;
using Microsoft.DirectX.DirectSound;

namespace FSDirectShow
{
	public class DirectSound
	{
		private Device device = null;
		private SecondaryBuffer secundaryBuffer = null;
		private DevicesCollection devicesCollection = new DevicesCollection();


		public void Initialize(IntPtr control, DeviceInfo deviceInfo)
		{
			try
			{
				Initialize(control, deviceInfo.DriverGuid);
			}
			catch (SoundException se)
			{
				throw new ExceptionUtil("Imposible inicializar DirectSound. Error: " + se.Message);
			}
		}

		public void Initialize(IntPtr control)
		{
			try
			{
				Initialize(control, devicesCollection[0].DriverGuid);
			}
			catch (SoundException se)
			{
				throw new ExceptionUtil("Imposible inicializar DirectSound. Error: " + se.Message);
			}
		}

		public void Initialize(IntPtr control, Guid guid)
		{
			try
			{
				device = new Device(guid);
				device.SetCooperativeLevel(control, CooperativeLevel.Priority);
			}
			catch (SoundException se)
			{
				throw new ExceptionUtil("Imposible inicializar DirectSound. Error: " + se.Message);
			}
		}

		public int Frequency
		{
			get { return secundaryBuffer != null ? secundaryBuffer.Frequency : 0; }
			set {
				if (secundaryBuffer != null)
					secundaryBuffer.Frequency = value;
			}
		}

		public int Volume
		{
			get { return secundaryBuffer != null ? secundaryBuffer.Volume : 0; }
			set { 
				if(secundaryBuffer!= null)
					secundaryBuffer.Volume = value;
			}
		}

		public int Pan
		{
			get { return secundaryBuffer != null ? secundaryBuffer.Pan : 0; }
			set {
				if (secundaryBuffer != null)
					secundaryBuffer.Pan = value;
			}
		}

		public List<DeviceInfo> Devices()
		{
			List<DeviceInfo> devices = new List<DeviceInfo>();
			foreach(DeviceInformation di in devicesCollection)
			{
				devices.Add(new DeviceInfo(di.DriverGuid, di.Description, di.ModuleName));
			}
			return devices;
		}

		public void Load(string fileName)
		{
			if (secundaryBuffer != null)
				secundaryBuffer.Dispose();
			secundaryBuffer = null;

			BufferDescription bufferDescription = new BufferDescription();
			bufferDescription.ControlFrequency = true;
			bufferDescription.ControlPan = true;
			bufferDescription.ControlVolume = true;
			bufferDescription.ControlEffects = false;
			//enables the audio to play even the window is minimized
			bufferDescription.GlobalFocus = true;
			//bufferDescription.Flags = BufferDescriptionFlags.ControlVolume | BufferDescriptionFlags.ControlFrequency | BufferDescriptionFlags.ControlPan | BufferDescriptionFlags.ControlEffects;

			try
			{
				secundaryBuffer = new SecondaryBuffer(fileName, bufferDescription, device);
			}
			catch (SoundException se)
			{
				throw new ExceptionUtil("Imposible cargar fichero (" + fileName + "). Error: " + se.Message);
			}
		}

		public void SetEffect(EffectList effect)
		{
			if (secundaryBuffer == null)
				return;

			EffectDescription[] fx = new EffectDescription[1]; // For this example use only 1 effect
															   // To use more effects, use more elements in the array
			fx[0] = new EffectDescription();

			switch (effect)
			{
				case EffectList.None:
					break;
				case EffectList.Chorus:
					fx[0].GuidEffectClass = DSoundHelper.StandardChorusGuid;
					secundaryBuffer.SetEffects(fx);
					//EchoEffect echoFx = (EchoEffect)secundaryBuffer.GetEffects(0);
					//EffectsEcho echoParams = echoFx.AllParameters;
					//echoParams.LeftDelay = 0;
					//echoParams.PanDelay = 0;
					//echoParams.RightDelay = 0;
					//echoFx.AllParameters = echoParams;
					break;
				case EffectList.Compressor:
					fx[0].GuidEffectClass = DSoundHelper.StandardCompressorGuid;
					secundaryBuffer.SetEffects(fx);
					break;
				case EffectList.Distortion:
					fx[0].GuidEffectClass = DSoundHelper.StandardDistortionGuid;
					secundaryBuffer.SetEffects(fx);
					break;
				case EffectList.Echo:
					fx[0].GuidEffectClass = DSoundHelper.StandardEchoGuid;
					secundaryBuffer.SetEffects(fx);
					break;
				case EffectList.Flanger:
					fx[0].GuidEffectClass = DSoundHelper.StandardFlangerGuid;
					secundaryBuffer.SetEffects(fx);
					break;
				case EffectList.Gargle:
					fx[0].GuidEffectClass = DSoundHelper.StandardGargleGuid;
					secundaryBuffer.SetEffects(fx);
					break;
				case EffectList.Interactive3D: // Interactive 3D Level 2 Reverb
					fx[0].GuidEffectClass = DSoundHelper.StandardInteractive3DLevel2ReverbGuid;
					secundaryBuffer.SetEffects(fx);
					break;
				case EffectList.ParamAqualizer:
					fx[0].GuidEffectClass = DSoundHelper.StandardParamEqGuid;
					secundaryBuffer.SetEffects(fx);
					//ParamEqEffect eqEffect = (ParamEqEffect)secundaryBuffer.GetEffects(0);
					//EffectsParamEq eqParams = eqEffect.AllParameters;
					//eqParams.Bandwidth = 36;
					//eqParams.Gain = ParamEqEffect.GainMax;
					//eqEffect.AllParameters = eqParams;
					break;
				case EffectList.WavesReverb:
					fx[0].GuidEffectClass = DSoundHelper.StandardWavesReverbGuid;
					secundaryBuffer.SetEffects(fx);
					break;
			}
		}

		public void Play()
		{
			BufferPlayFlags playFlags = 0;

			secundaryBuffer.Play(0, playFlags);
		}


		public bool IsPlaying()
		{
			if (secundaryBuffer != null)
			{
				return secundaryBuffer.Status.Playing || secundaryBuffer.Status.Looping;
			}

			return false;
		}

		public void Stop()
		{
			if (secundaryBuffer != null)
			{
				secundaryBuffer.Stop();
				//rebobinar
				secundaryBuffer.SetCurrentPosition(0);
			}
		}

		public void Pause()
		{
			if (secundaryBuffer != null)
			{
				secundaryBuffer.Stop();
			}
		}

		public int SamplesPerSecond
		{
			get { return (secundaryBuffer != null ? secundaryBuffer.Format.SamplesPerSecond : 0); }
		}

		public int BitsPerSample
		{
			get { return (secundaryBuffer != null ? secundaryBuffer.Format.BitsPerSample : 0); }
		}
		public int Channels
		{
			get { return (secundaryBuffer != null ? secundaryBuffer.Format.Channels : 0); }
		}
		public int AverageBytesPerSecond
		{
			get { return (secundaryBuffer != null ? secundaryBuffer.Format.AverageBytesPerSecond : 0); }
		}
		public int Duration
		{
			get { return (secundaryBuffer != null ? (int)(secundaryBuffer.Caps.BufferBytes / secundaryBuffer.Format.AverageBytesPerSecond) : 0); }
		}
		public int BufferBytes
		{
			get { return (secundaryBuffer != null ? secundaryBuffer.Caps.BufferBytes : 0); }
		}

		public int PlayPosition
		{
			get { return (secundaryBuffer != null ? secundaryBuffer.PlayPosition : 0); }
		}

		public void SetCurrentPosition(int position)
		{
			if (secundaryBuffer != null)
			{
				secundaryBuffer.SetCurrentPosition(position);
			}
		}


		public string TimeTotalString()
		{
			if (secundaryBuffer == null)
				return "0:00";

			int toth = 0;
			int totm = 0;
			int tots = 0;

			int tot = Duration;
			toth = tot / 3600;
			totm = (tot - (toth * 3600)) / 60;
			tots = tot - (toth * 3600) - (totm * 60);

			return toth.ToString() + ":" + totm.ToString() + ":" + string.Format("{0:00}", tots);
		}

		public string TimePositionString()
		{
			if (secundaryBuffer == null)
				return "0:00";

			int h = 0;
			int m = 0;
			int s = 0;

			int now = PlayPosition / AverageBytesPerSecond;
			h = now / 3600;
			m = (now - (h * 3600)) / 60;
			s = now - (h * 3600) - (m * 60);

			return h.ToString() + ":" + m.ToString() + ":" + string.Format("{0:00}", s);
		}


		public void SpeakerConfig(bool stereo, bool headphone, bool quad, bool fivePointOne, bool sevenPointOne)
		{
			if (device != null)
			{
				Speakers speaker = new Speakers();
				speaker.Stereo = stereo;
				speaker.Headphone = headphone;
				speaker.Quad = quad;
				speaker.FivePointOne = fivePointOne;
				speaker.SevenPointOne = sevenPointOne;

				device.SpeakerConfig = speaker;
			}
		}
	}

	public class DeviceInfo
	{
		public Guid DriverGuid;
		public string Description;
		public string ModuleName;

		public DeviceInfo(Guid guid, string description, string moduleName)
		{
			this.DriverGuid = guid;
			this.Description = description;
			this.ModuleName = moduleName;
		}
	}

	public enum EffectList
	{
		None,
		Chorus,
		Compressor,
		Echo,
		Distortion,
		Flanger,
		Gargle,
		Interactive3D,
		WavesReverb,
		ParamAqualizer
	}
}
