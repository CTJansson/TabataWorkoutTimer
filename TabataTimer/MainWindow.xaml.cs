using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TabataTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private int _prepareTime = 20;
        private int _workTime = 20;
        private int _restTime = 10;
        private int _cyclesCount = 8;
        private int _tabataCount = 1;

        MediaPlayer mp = new MediaPlayer();

        private void buttonPrepare_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button.Name == "buttonIncreasePrepare")
                _prepareTime += 5;
            else
                if (_prepareTime > 0)
                _prepareTime -= 5;

            if (_prepareTime == 1)
                textBoxPrepare.Text = _prepareTime + " Second";
            else
                textBoxPrepare.Text = _prepareTime + " Seconds";
        }

        private void buttonWork_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button.Name == "buttonIncreaseWork")
                _workTime++;
            else
                if (_workTime > 0)
                _workTime--;

            if (_workTime == 1)
                textBoxWork.Text = _workTime + " Second";
            else
                textBoxWork.Text = _workTime + " Seconds";
        }

        private void buttonRest_Clickl(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button.Name == "buttonIncreaseRest")
                _restTime++;
            else
                if (_restTime > 0)
                _restTime--;

            if (_restTime == 1)
                textBoxRest.Text = _restTime + " Second";
            else
                textBoxRest.Text = _restTime + " Seconds";
        }

        private void buttonCycles_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button.Name == "buttonIncreaseCycles")
                _cyclesCount++;
            else
                if (_cyclesCount > 1)
                _cyclesCount--;

            if (_cyclesCount == 1)
                textBoxCycles.Text = _cyclesCount + " Cycle";
            else
                textBoxCycles.Text = _cyclesCount + " Cycles";
        }

        private void buttonTabatas_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button.Name == "buttonIncreaseTabatas")
                _tabataCount++;
            else
                if (_tabataCount > 1)
                _tabataCount--;

            if (_tabataCount == 1)
                textBoxTabatas.Text = _tabataCount + " Tabata";
            else
                textBoxTabatas.Text = _tabataCount + " Tabatas";
        }

        // Tab 2

        private DispatcherTimer _workoutClock;

        private int _workSeconds;
        private int _restSeconds;
        private int _prepareSeconds;
        private int _cyclesCompleted = 0;
        private int _tabatasCompleted = 0;

        private bool _prepareIsGo = true;
        private bool _workoutIsGo = false;
        private bool _restIsGo = false;

        private string _path;

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            _workSeconds = _workTime + 1;
            _restSeconds = _restTime + 1;
            _prepareSeconds = _prepareTime;

            labelWorkTimer.Content = _workSeconds -1 + "s";
            labelRestTimer.Content = _restSeconds - 1 + "s";

            _workoutClock = new DispatcherTimer(DispatcherPriority.Normal);
            _workoutClock.Interval = new TimeSpan(0, 0, 0, 1);
            _workoutClock.Tick += _workoutClock_Tick;
            _workoutClock.Start();

            tabControl.SelectedIndex = 1;
        }

        private void _workoutClock_Tick(object sender, EventArgs e)
        {
            if (_prepareIsGo)
            {
                _prepareSeconds--;

                switch (_prepareSeconds - 1)
                {
                    case 15:
                        _path = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sounds\\15toworkout.mp3");
                        mp.Open(new Uri(_path));
                        mp.Play();
                        break;
                    case 3:
                        _path = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sounds\\three.mp3");
                        mp.Open(new Uri(_path));
                        mp.Play();
                        break;
                    case 2:
                        _path = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sounds\\two.mp3");
                        mp.Open(new Uri(_path));
                        mp.Play();
                        break;
                    case 1:
                        _path = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sounds\\one.mp3");
                        mp.Open(new Uri(_path));
                        mp.Play();
                        break;
                    case 0:
                        _path = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sounds\\workout.mp3");
                        mp.Open(new Uri(_path));
                        mp.Play();
                        break;
                }

                if (_prepareSeconds == 0)
                    labelPrepareTimer.Content = _prepareSeconds + "s";
                else
                    labelPrepareTimer.Content = _prepareSeconds - 1 + "s";

                if (_prepareSeconds == 0)
                {
                    _prepareIsGo = false;
                    _workoutIsGo = true;
                }
            }

            if (_workoutIsGo)
            {
                _workSeconds--;

                switch (_workSeconds - 1)
                {
                    case 3:
                        _path = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sounds\\three.mp3");
                        mp.Open(new Uri(_path));
                        mp.Play();
                        break;
                    case 2:
                        _path = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sounds\\two.mp3");
                        mp.Open(new Uri(_path));
                        mp.Play();
                        break;
                    case 1:
                        _path = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sounds\\one.mp3");
                        mp.Open(new Uri(_path));
                        mp.Play();
                        break;
                    case 0:
                        _cyclesCompleted++;
                        labelCycleCount.Content = _cyclesCompleted;
                        _path = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sounds\\cycle" + _cyclesCompleted + "complete.mp3");
                        mp.Open(new Uri(_path));
                        mp.Play();
                        break;
                }



                if (_workSeconds == 0)
                    labelWorkTimer.Content = _workSeconds + "s";
                else if (_workSeconds != 0 && _cyclesCompleted > 0)
                    labelWorkTimer.Content = _workSeconds - 1 + "s";
                else
                    labelWorkTimer.Content = -1 + _workSeconds + "s";

                if (_workSeconds == 0)
                {
                    _workSeconds = _workTime + 1;
                    labelWorkTimer.Content = _workSeconds - 1 + "s";
                    _workoutIsGo = false;
                    _restIsGo = true;

                    if (_cyclesCompleted == 8)
                        _restSeconds = 60;
                }
            }

            if (_restIsGo)
            {
                _restSeconds--;

                switch (_restSeconds - 1)
                {
                    case 15:
                        _path = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sounds\\15toworkout.mp3");
                        mp.Open(new Uri(_path));
                        mp.Play();
                        break;
                    case 3:
                        _path = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sounds\\three.mp3");
                        mp.Open(new Uri(_path));
                        mp.Play();
                        break;
                    case 2:
                        _path = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sounds\\two.mp3");
                        mp.Open(new Uri(_path));
                        mp.Play();
                        break;
                    case 1:
                        _path = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sounds\\one.mp3");
                        mp.Open(new Uri(_path));
                        mp.Play();
                        break;
                    case 0:
                        _path = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sounds\\workout.mp3");
                        mp.Open(new Uri(_path));
                        mp.Play();
                        break;
                }

                //if (_restSeconds - 1 == 0 && _cyclesCompleted != 8)
                //{
                //    _path = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sounds\\Workout.mp3");
                //    mp.Open(new Uri(_path));
                //    mp.Play();
                //}

                if (_restSeconds == 0)
                    labelRestTimer.Content = _restSeconds + "s";
                else
                    labelRestTimer.Content = -1 + _restSeconds + "s";

                if (_restSeconds == 0)
                {
                    _restIsGo = false;
                    _workoutIsGo = true;

                    _restSeconds = _restTime + 1;

                    if (_cyclesCompleted != 8)
                    {
                        _workSeconds--;
                        labelWorkTimer.Content = _workSeconds - 1 + "s";
                    }

                    labelRestTimer.Content = _restSeconds - 1 + "s";

                    if (_cyclesCompleted == _cyclesCount)
                    {
                        _tabatasCompleted++;
                        _cyclesCompleted = 0;

                        labelTabataCount.Content = _tabatasCompleted;
                        labelCycleCount.Content = _cyclesCompleted;

                        //_workoutIsGo = true;
                        //_prepareIsGo = false;
                    }
                }
            }
        }

        private void butonStopWorkout_click(object sender, RoutedEventArgs e)
        {
        }
    }
}
