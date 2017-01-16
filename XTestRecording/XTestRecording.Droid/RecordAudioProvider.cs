using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XTestRecording;
using Android.Media;
using System.Threading.Tasks;
using Xamarin.Forms;
using Android.Util;

[assembly: Dependency(typeof(XTestRecording.Droid.RecordAudioProvider))]

namespace XTestRecording.Droid
{
    public class RecordAudioProvider: IRecordAudioProvider
    {
        private MediaRecorder _mediaRecorder;

        public void StartRecordingSound(string filename)
        {
            if (_mediaRecorder == null)
                _mediaRecorder = new MediaRecorder();
            else
                _mediaRecorder.Reset();

            _mediaRecorder.SetAudioSource(AudioSource.Mic);
            //_mediaRecorder.SetOutputFormat(OutputFormat.Default);
            //_mediaRecorder.SetAudioEncoder(AudioEncoder.Default);
            _mediaRecorder.SetOutputFormat(OutputFormat.ThreeGpp);
            _mediaRecorder.SetAudioEncoder(AudioEncoder.AmrNb);
            _mediaRecorder.SetOutputFile(filename);
            _mediaRecorder.Prepare();
            _mediaRecorder.Start();

            Log.Info("StartRecordingSound", filename);
        }

        public void Stop()
        {
            if (_mediaRecorder == null)
                return;

            _mediaRecorder.Stop();
            _mediaRecorder.Reset();

            Log.Info("RecordAudioProvider", "Stopped recording sound");
        }

        public void Start(string filename)
        {
            StartRecordingSound(filename);

            //var tcs = new TaskCompletionSource<object>();
            //tcs.SetResult(null);
            //return tcs.Task;
        }

        public void Dispose()
        {
            if (_mediaRecorder != null)
            {
                _mediaRecorder.Release();
                _mediaRecorder.Dispose();
                _mediaRecorder = null;
            }
        }
    }
}