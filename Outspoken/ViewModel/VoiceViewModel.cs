using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;

namespace Outspoken.ViewModel
{
    class VoiceViewModel
    {
        private VoiceInformation _voiceInformation;

        public VoiceViewModel(VoiceInformation voiceInformation)
        {
            this._voiceInformation = voiceInformation;
        }

        public double AudioPitch { get; internal set; }
        public double SpeakingRate { get; internal set; }

        public string DisplayName {
            get { return this._voiceInformation.DisplayName; }
        }

        public VoiceInformation VoiceInformation
        {
            get { return _voiceInformation; }
        }

        internal static ObservableCollection<VoiceViewModel> PopulateVoices()
        {
            var voices = new ObservableCollection<VoiceViewModel>();
            foreach (var voice in SpeechSynthesizer.AllVoices)
            {
                voices.Add(new VoiceViewModel(voice));
            }
            return voices;
        }
    }
}
