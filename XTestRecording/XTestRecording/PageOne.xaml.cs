using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XTestRecording
{
    public partial class PageOne : ContentPage
    {
        private IPlayAudioProvider _playAudioService;
        private IRecordAudioProvider _recordAudioService;
        private IFileServiceProvider _fileService;

        private string _audioFileName;
        private bool _isPlaying = false;
        private bool _isRecording = false;

        public PageOne()
        {
            InitializeComponent();

            _playAudioService = DependencyService.Get<IPlayAudioProvider>();
            _playAudioService.PlayAudioCompleted += _playAudioService_PlayAudioCompleted;

            _recordAudioService = DependencyService.Get<IRecordAudioProvider>();
            _fileService = DependencyService.Get<IFileServiceProvider>();            
        }

        public void OnStartRecording(object sender, EventArgs e)
        {
            if (!_isRecording)
            {
                // start recording
                _isRecording = true;
                recordButton.Text = "Stop Recording";
                playButton.IsEnabled = false;

                _audioFileName = _fileService.GetFileFullPath("test.3gpp");
                if (_fileService.FileExists(_audioFileName))
                    _fileService.DeleteFile(_audioFileName);

                _recordAudioService.Start(_audioFileName);
            }
            else
            {
                // stop recording
                _isRecording = false;
                recordButton.Text = "Start Recording";
                playButton.IsEnabled = true;
                _recordAudioService.Stop();
            }
        }

        public void OnPlayRecording(object sender, EventArgs e)
        {
            if (!_isPlaying)
            {
                // start playing sound
                if (!_fileService.FileExists(_audioFileName))
                {
                    DisplayAlert("Play Audio", "File does not exist: " + _audioFileName, "OK");
                    return;
                }

                _isPlaying = true;
                playButton.Text = "Stop Playing";
                recordButton.IsEnabled = false;

                _playAudioService.StartAsync(_audioFileName);
            }
            else
            {
                // stop playing sound
                _isPlaying = false;
                playButton.Text = "Start Playing";
                recordButton.IsEnabled = true;

                _playAudioService.Stop();
            }

        }

        private void _playAudioService_PlayAudioCompleted(object sender, EventArgs e)
        {
            _isPlaying = false;
            playButton.Text = "Start Playing";
            recordButton.IsEnabled = true;
        }
    }
}
