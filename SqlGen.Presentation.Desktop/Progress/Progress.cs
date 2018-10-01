using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SqlGen;
using SqlGen.Presentation.Desktop.Progress;


namespace Metaproject.Controls
{
	public class Progress : IProgress, IDisposable
	{
	    private readonly Form _parent;

	    #region <singleton>

	    public Progress()
	    {
	        if (null == _progress) _progress = new FormProgress();
	    }
        
	   #endregion

		#region <members>
		static FormProgress _progress;
        Counter _counter = null;
        int _counterSteps = -1;
		#endregion

        #region <pub>
        
        public void Show(string header)
        {
            if (null == _progress) _progress = new FormProgress();
            _progress.LabelHeader = header;
            _progress.ShowProgress(null);
        }

        public void Close()
        {
            _progress.CloseProgress();
            _counterSteps = -1;
        }
        
        public void SetUseCounter(int numberOfSteps)
        {
            _counterSteps = numberOfSteps;
        }

        #endregion

        #region <IProgress>

	    public void Show(string info, int step, int total)
	    {
            
	        _progress.Show();
            _progress.LabelText = info;
	        _progress.Steps = total;
            _progress.Step(step+1);
	        
        }

        void Set(string caption, int steps)
		{
            _progress.LabelText = caption;

            if (_counterSteps > 0)
            {
                _counter = new Counter(_counterSteps, steps);
                _progress.Steps = _counterSteps;
            }
            else
            {
                _progress.Steps = steps;
            }
			
		}
        

        #endregion

        #region <IDisposable>

        public void Dispose()
        {
            Close();
        }

        public void HideProgress()
        {
            throw new NotImplementedException();
        }
        #endregion

        public class Counter
	    {

	        int _counter = 0;
	        int _hitNumber = 0;

	        public Counter(int numberOfSteps, int total)
	        {
	            NumberOfSteps = numberOfSteps;
	            Total = total;

	            _hitNumber = (Total / NumberOfSteps);

	        }


	        public bool IsHit()
	        {
	            _counter++;

	            if (_counter == _hitNumber)
	            {
	                _counter = 0;
	                return true;
	            }

	            return false;
	        }


	        public int NumberOfSteps { get; set; }
	        public int Total { get; set; }

	    }

	  
	}
}
