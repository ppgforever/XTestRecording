using XTestRecording;
using Android.Media;
using Xamarin.Forms;
using System;
using System.Threading.Tasks;

[assembly: Dependency(typeof(XTestRecording.Droid.PlayAudioProvider))]

namespace XTestRecording.Droid
{
    public delegate void PlayAudioCompletedEventHandler();

    public class PlayAudioProvider : IPlayAudioProvider
    {
        private MediaPlayer _mediaPlayer;

        public bool IsPlaying
        {
            get { return (_mediaPlayer != null && _mediaPlayer.IsPlaying); }
        }

        public event EventHandler PlayAudioCompleted;

        public async Task PlaySoundAsync(string filename)
        {
            if (_mediaPlayer == null)
            {
                _mediaPlayer = new MediaPlayer();
                _mediaPlayer.Completion += _mediaPlayer_Completion;
            }
            else
                _mediaPlayer.Reset();

            var fd = Xamarin.Forms.Forms.Context.Assets.OpenFd(filename);

            _mediaPlayer.Prepared += (s, e) =>
            { _mediaPlayer.Start(); };

            await _mediaPlayer.SetDataSourceAsync(fd.FileDescriptor, fd.StartOffset, fd.Length);
            _mediaPlayer.Prepare();
        }

        private void _mediaPlayer_Completion(object sender, EventArgs e)
        {
            PlayAudioCompleted(this, e);
        }

        public void StopPlayingSound()
        {
            if (_mediaPlayer == null) return;

            if (_mediaPlayer.IsPlaying)
            {
                _mediaPlayer.Stop();
            }
        }

        public void Dispose()
        {
            if (_mediaPlayer != null)
            {
                _mediaPlayer.Release();
                _mediaPlayer = null;
            }
        }

        public async Task StartAsync(string filename)
        {
            await PlaySoundAsync(filename);
        }

        public void Stop()
        {
            StopPlayingSound();
        }
    }
}