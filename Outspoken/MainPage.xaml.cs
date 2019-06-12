using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Media.SpeechSynthesis;
using Outspoken.ViewModel;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Outspoken
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MediaElement _media;
        SpeechSynthesizer _speech;

        ObservableCollection<VoiceViewModel> Voices = VoiceViewModel.PopulateVoices();
        VoiceViewModel SelectedVoice;

        public MainPage()
        {
            this.InitializeComponent();

            _media = new MediaElement();
            _speech = new SpeechSynthesizer();

            SelectedVoice = Voices[0];
            _speech.Voice = SelectedVoice.VoiceInformation;
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SpeakAndClearAsync();
        }

        private void MessageBox_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                SpeakAndClearAsync();
            }
        }

        private async void SpeakAndClearAsync()
        {
            SpeechSynthesisStream stream = await _speech.SynthesizeTextToStreamAsync(MessageBox.Text);

            _media.SetSource(stream, stream.ContentType);
            _media.Play();

            MessageBox.Text = "";
        }

        private void SelectedVoiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedVoice = e.AddedItems[0] as VoiceViewModel;
            _speech.Voice = SelectedVoice.VoiceInformation;
        }

        private void AudioPitch_Slider_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            if (SelectedVoice != null)
            {
                SelectedVoice.AudioPitch = e.NewValue;
                _speech.Options.AudioPitch = SelectedVoice.AudioPitch;
            }
        }

        private void SpeakingRate_Slider_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            if (SelectedVoice != null)
            {
                SelectedVoice.SpeakingRate = e.NewValue;
                _speech.Options.SpeakingRate = SelectedVoice.SpeakingRate;
            }
        }
    }
}
