using Mandelbrot.Calculations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Input;

namespace Mandelbrot.UWP.ViewModels
{
    internal class MandelbrotParameters : INotifyPropertyChanged
    {
        private readonly MainPage _window;

        public MandelbrotParameters(MainPage window)
        {
            _window = window;
            Reset();
        }

        private string _executiontime;
        private float _top;
        private float _left;
        private float _bottom;
        private float _right;
        private int _iterations;

        public float Top
        {
            get => _top;
            set
            {
                _top = value;
                RaisePropertyChanged("Top");
            }
        }

        public float Left
        {
            get => _left;
            set
            {
                _left = value;
                RaisePropertyChanged("Left");
            }
        }

        public float Bottom
        {
            get => _bottom;
            set
            {
                _bottom = value;
                RaisePropertyChanged("Bottom");
            }
        }

        public float Right
        {
            get => _right;
            set
            {
                _right = value;
                RaisePropertyChanged("Right");
            }
        }

        public int Iterations
        {
            get => _iterations;
            set
            {
                _iterations = value;
                RaisePropertyChanged("Iterations");
            }
        }

        public string Executiontime
        {
            get => _executiontime;
            set
            {
                _executiontime = value;
                RaisePropertyChanged("Executiontime");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #region Reset command

        private RelayCommand _resetCommand;

        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                {
                    _resetCommand = new RelayCommand(param => Reset(), param => true);
                }
                return _resetCommand;
            }
        }

        private void Reset()
        {
            Executiontime = "00:00:00";
            Top = -1.6f;
            Left = -2.5f;
            Bottom = 1.6f;
            Right = 1.0f;
            Iterations = 500;
        }

        #endregion Reset command

        #region Calculate command

        private RelayCommand _calculateCommand;

        public ICommand CalculateCommand
        {
            get
            {
                if (_calculateCommand == null)
                {
                    _calculateCommand = new RelayCommand(param => Calculate(), param => true);
                }
                return _calculateCommand;
            }
        }

        private void Calculate()
        {
            DateTime start = DateTime.Now;
            RectangleF drawing = new RectangleF(_left, _top, _right - _left, _bottom - _top);
            RectangleF viewport = _window.GetImageViewport();
            IEnumerable<FractalPoint> pts = MandelbrotFractal.Calculate(viewport, drawing, _iterations);
            TimeSpan duration = DateTime.Now - start;
            Executiontime = duration.ToString(@"mm\:ss\.fff");

            _window.DrawImage(pts);
        }

        #endregion Calculate command

        private void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}