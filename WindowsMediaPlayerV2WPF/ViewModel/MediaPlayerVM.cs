
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Input;
using WindowsMediaPlayerV2Core;

namespace WindowsMediaPlayerV2.ViewModel
{
    public class MediaPlayerVM : MVVM.ViewModel
    {
        private Program WMP;
        private string _fPath { get; set; }
        private string _fExt { get; set; }
        private string _fName { get; set; }
        private Dictionary<String, Type> ExtToType;
        private Media _toPlay;
        private int _width, _height;
        public MediaPlayerVM()
        {
            WMP = new Program();
            ExtToType = new Dictionary<string, Type>();
            ExtToType["mp3"] = typeof(WindowsMediaPlayerV2Core.Sound);
            ExtToType["wav"] = typeof(WindowsMediaPlayerV2Core.Sound);
            ExtToType["mp4"] = typeof(WindowsMediaPlayerV2Core.Video);
            ExtToType["avi"] = typeof(WindowsMediaPlayerV2Core.Video);
            ExtToType["jpeg"] = typeof(WindowsMediaPlayerV2Core.Photo);
            ExtToType["jpg"] = typeof(WindowsMediaPlayerV2Core.Photo);
            ExtToType["png"] = typeof(WindowsMediaPlayerV2Core.Photo);
        }
        public int Width
        {
            get { return _width; }
        }
        public int Height
        {
            get { return _height; }
        }
        public String FilePath
        {
            get { return _fPath; }
            set
            {
                _fPath = value;
                int index = _fPath.LastIndexOf('\\');
                String fNameWithExt = FilePath.Substring(index + 1);
                index = fNameWithExt.IndexOf('.');
                _fName = fNameWithExt.Substring(0, index);
                _fExt = fNameWithExt.Substring(index + 1);
            }
        }
        public Media ToPlay
        {
            get { return _toPlay; }
            set
            {
                _toPlay = value;
                RaisePropertyChangedEvent("ToPlay");
            }
        }
        public ICommand PlayCommand
        {
            get
            {
                return new MVVM.DelegateCommand(() =>
                {
                    MethodInfo PlayMeth = WMP.player.GetType().GetMethod("Play");
                    PlayMeth.Invoke(WMP.player, null);
                });
            }
        }
        public ICommand PauseCommand
        {
            get
            {
                return new MVVM.DelegateCommand(() =>
                    {
                        MethodInfo PauseMeth = WMP.player.GetType().GetMethod("Pause");
                        PauseMeth.Invoke(WMP.player, null);
                    });
            }
        }
        public ICommand StopCommand
        {
            get
            {
                return new MVVM.DelegateCommand(() =>
                    {
                        MethodInfo StopMeth = WMP.player.GetType().GetMethod("Stop");
                        StopMeth.Invoke(WMP.player, null);

                    });
            }
        }
        public ICommand NextMediaCommand
        {
            get
            {
                return new MVVM.DelegateCommand(() =>
                {
                    //MethodInfo NextMeth = WMP.player.GetType().GetMethod
                });
            }
        }
        public ICommand CreateMediaCommand
        {
            get
            {
                return new MVVM.DelegateCommand(() =>
                {
                    try
                    {
                       
                    }
                    catch (Exception) { }
                });
            }
        }
        private void CreateMedia()
        {
            MethodInfo meth = WMP.player.GetType().GetMethod("CreateMedia");
            MethodInfo MediaCreate = meth.MakeGenericMethod(ExtToType[_fExt]);
            ToPlay = (Media)MediaCreate.Invoke(WMP.player, new String[] { _fPath, _fName, _fExt });
        }
        public ICommand OpenFileCommand
        {
            get
            {
                return new MVVM.DelegateCommand(() =>
                {
                    var dlg = new Microsoft.Win32.OpenFileDialog();
                    dlg.DefaultExt = ".mp3";
                    dlg.Filter = "Multimedia Files (*.mp3, *.wav, *.mp4, *.avi, *.jpg, *.jpeg, *.png)|*.mp3;*.wav;*.mp4;*.avi;*.jpg;*.jpeg;*.png";

                    Nullable<bool> result = dlg.ShowDialog();
                    if (result == true)
                    {
                        FilePath = dlg.FileName;
                        CreateMedia();
                    }
                });
            }
        }
        public ICommand OpenDirCommand
        {
            get
            {
                return new MVVM.DelegateCommand(() =>
                {
                    var dlg = new CommonOpenFileDialog();
                    dlg.IsFolderPicker = true;
                    CommonFileDialogResult result = dlg.ShowDialog();
                    if (result == CommonFileDialogResult.Ok)
                    {
                        String[] files = System.IO.Directory.GetFiles(dlg.FileName);
                        foreach (var f in files)
                        {
                            FilePath = f;
                            if (CreateMediaCommand.CanExecute(null))
                                CreateMediaCommand.Execute(null);
                        }
                    }
                });
            }
        }
        public ICommand SelectTitleFromPlaylistCommand
        {
            get
            {
                return new MVVM.DelegateCommand(null);
            }
        }
        public IEnumerable<Media> Playlist
        {
            get 
            {
                //var PlaylistField = WMP.player.GetType().GetField("Playlist");
                //return PlaylistField.GetValue(WMP.player) as IEnumerable<Media>; 
                return WMP.player.Playlist;
            }
        }
    }
}