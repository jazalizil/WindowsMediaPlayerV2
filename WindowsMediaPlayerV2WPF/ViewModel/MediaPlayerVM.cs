
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
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
        private double _centerGridHeight;
        private readonly String PauseButtonData = "F1M7,6C7,6 12,9 12,9 12,9 7,12 7,12 7,12 7,6 7,6z M9,3C5.686,3 3,5.686 3,9 3,12.314 5.686,15 9,15 12.314,15 15,12.314 15,9 15,5.686 12.314,3 9,3z M9,1C13.418,1 17,4.582 17,9 17,13.418 13.418,17 9,17 4.582,17 1,13.418 1,9 1,4.582 4.582,1 9,1z";
        private readonly String PlayButtonData = "F1M10,7C10,7 12,7 12,7 12,7 12,11 12,11 12,11 10,11 10,11 10,11 10,7 10,7z M6,7C6,7 8,7 8,7 8,7 8,11 8,11 8,11 6,11 6,11 6,11 6,7 6,7z M9,3C5.686,3 3,5.686 3,9 3,12.314 5.686,15 9,15 12.314,15 15,12.314 15,9 15,5.686 12.314,3 9,3z M9,1C13.418,1 17,4.582 17,9 17,13.418 13.418,17 9,17 4.582,17 1,13.418 1,9 1,4.582 4.582,1 9,1z";
        private String _buttonData;
        public MediaPlayerVM()
        {
            WMP = new Program();
            ExtToType = new Dictionary<string, Type>();
            CenterGridHeight = 580;
            _buttonData = PauseButtonData;
            ExtToType["mp3"] = typeof(WindowsMediaPlayerV2Core.Sound);
            ExtToType["wav"] = typeof(WindowsMediaPlayerV2Core.Sound);
            ExtToType["mp4"] = typeof(WindowsMediaPlayerV2Core.Video);
            ExtToType["avi"] = typeof(WindowsMediaPlayerV2Core.Video);
            ExtToType["jpeg"] = typeof(WindowsMediaPlayerV2Core.Photo);
            ExtToType["jpg"] = typeof(WindowsMediaPlayerV2Core.Photo);
            ExtToType["png"] = typeof(WindowsMediaPlayerV2Core.Photo);
        }

        public String ButtonData
        {
            get
            {
                return _buttonData;
            }
            set
            {
                _buttonData = value;
                RaisePropertyChangedEvent("ButtonData");
            }
        }
        public String UserName
        {
            get { return Environment.UserName; }
        }
        public String MachineName
        {
            get { return Environment.MachineName; }
        }
        public Double CenterGridHeight
        {
            get { return _centerGridHeight; }
            set
            {
                _centerGridHeight = value;
                RaisePropertyChangedEvent("CenterGridHeight");
            }

        }
        public String FilePath
        {
            get { return _fPath; }
            set
            {
                _fPath = value;
                int index = _fPath.LastIndexOf('\\');
                String fNameWithExt = FilePath.Substring(index + 1);
                index = fNameWithExt.LastIndexOf('.');
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
        public Media ToSwitch
        {
            set
            {
                ToPlay = value;
                MethodInfo SwitchMeth = WMP.player.GetType().GetMethod("SwitchMedia");
                SwitchMeth.Invoke(WMP.player, new Media[]{value});
            }
        }
        public ICommand PlayPauseCommand
        {
            get
            {
                return new MVVM.DelegateCommand(() =>
                {
                    String cmd;
                    if (_buttonData == PlayButtonData)
                    {
                        cmd = "Pause";
                        ButtonData = PauseButtonData;
                    }
                    else
                    {
                        cmd = "Play";
                        ButtonData = PlayButtonData;
                    }
                    MethodInfo PlayMeth = WMP.player.GetType().GetMethod(cmd);
                    PlayMeth.Invoke(WMP.player, null);
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
        public ICommand ResizeCommand
        {
            get
            {
                return new MVVM.DelegateCommand(() =>
                {
                    CenterGridHeight = Application.Current.MainWindow.ActualHeight - 170;
                });
            }
        }
        public ICommand NextMediaCommand
        {
            get
            {
                return new MVVM.DelegateCommand(() =>
                {
                    MessageBox.Show("sss");
                    //MethodInfo NextMeth = WMP.player.GetType().GetMethod
                    //int index = Playlist.
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
                    dlg.Filter = "Multimedia Files (*.mp3, *.wav, *.mp4, *.avi, *.jpg, *.jpeg, *.png)|*.mp3;*.wav;*.mp4;*.avi;*.jpg;*.jpeg;*.png";

                    Nullable<bool> result = dlg.ShowDialog();
                    if (result == true)
                    {
                        FilePath = dlg.FileName;
                        CreateMedia();
                        ButtonData = PlayButtonData;
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
                            try
                            {
                                CreateMedia();
                            }
                            catch (KeyNotFoundException) { }
                        }
                        ToPlay = Playlist.First();
                        ButtonData = PlayButtonData;
                    }
                });
            }
        }


        public IEnumerable<Media> Playlist
        {
            get
            {
                //var PlaylistField = WMP.player.GetType().GetFields();
                return WMP.player.Playlist;
            }
        }
    }
}