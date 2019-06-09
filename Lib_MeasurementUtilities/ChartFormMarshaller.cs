using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lib_MeasurementUtilities
{
    public class ChartFormMarshaller
    {
        private List<Button> _chartFormbuttons = new List<Button>();
        private List<ChartForm> _chartForms = new List<ChartForm>();
        private Button _previousButton = null;
        private Form _parent;
        private readonly int _numChartForms;
        private List<List<double>> _dataBuffers;
        private Button _btnRest;
        private readonly int _numItemsInOneRound;
        private readonly bool _reversed;
        private readonly int _endIndex, _startIndex;

        public int currentIndex { get; set; }
        public object mu_currentIndex { get; set; } 


        public ChartFormMarshaller(Form parent, List<Tuple<string, Color>> legendSettings, List<double> sigmas, int numSamples, List<Button> chartFormButtons, Button btnReset, bool reversed = false)
        {
            _parent = parent;
            Debug.Assert(chartFormButtons.Count == sigmas.Count);
            _numChartForms = chartFormButtons.Count;
            _chartFormbuttons = chartFormButtons;
            _btnRest = btnReset;
            _numItemsInOneRound = legendSettings.Count;
            mu_currentIndex = new object();
            _reversed = reversed;
            _endIndex = reversed ? 0 : _numItemsInOneRound+1;
            _startIndex = reversed ? _numItemsInOneRound : 1;
            currentIndex = _startIndex;

            // create a chartForm for each button
            for (int i = 0; i < _numChartForms; i++)
            {
                _chartForms.Add(new ChartForm(numSamples, sigmas[i], _chartFormbuttons[i].Text, legendSettings));
            }

            // associate each button with a chartForm
            foreach (var button in _chartFormbuttons)
            {
                button.Click += button_Click;
            }

            _btnRest.Click += _btnRest_Click;


            initDataBuffers();
        }

  

        void _btnRest_Click(object sender, EventArgs e)
        {
            clearDataBuffers();
            foreach (var chartForm in _chartForms)
            {
                chartForm.resetSummery();
            }

            lock (mu_currentIndex)
            {
                currentIndex = _startIndex;
            }
        }



        void button_Click(object sender, EventArgs e)
        {
            if (sender == _previousButton) return;

            for (int i = 0; i < _numChartForms; i++)
            {
                if (sender == _chartFormbuttons[i])
                {
                    _chartForms[i].Show(_parent);
                }
                else
                {
                    _chartForms[i].Hide();
                }
            }

            _previousButton = (Button)sender;
        }

        public void collectData(params double[] data)
        {
            for (int i = 0; i < _numChartForms; i++)
            {
                _dataBuffers[i].Add(data[i]);
            }
        }

        public void GenerateSummery()
        {
            for (int i = 0; i < _numChartForms; i++)
            {
                Console.WriteLine(_dataBuffers[i].Count);
                _chartForms[i].updateSeries_Invoke(_dataBuffers[i]);
            }
        }

        public void clearDataBuffers()
        {
            foreach (var dataBuffer in _dataBuffers)
            {
                dataBuffer.Clear();
            }
        }

        private void initDataBuffers()
        {
            _dataBuffers = new List<List<double>>();
            for (int i = 0; i < _numChartForms; i++)
            {
                _dataBuffers.Add(new List<double>());
            }
        }

        public void incrementItemCount()
        {
            if (_reversed)
            {
                currentIndex--;
            }
            else
            {
                currentIndex++;
            }
        }

        public bool RoundEndReached()
        {
            return currentIndex == _endIndex;
        }

        public void resetItemCount()
        {
            currentIndex = _startIndex;
        }
    }
}
