using Acr.UserDialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace HelloPrism.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        IUserDialogs _userDialogs;
        public MainPageViewModel(IUserDialogs userDialogs)
        {
            _userDialogs = userDialogs;
        }

        private string _firstValue;
        public string FirstValue
        {
            get => _firstValue;
            set => SetProperty(ref _firstValue, value);
        }

        private string _secondValue; 
        public string SecondValue
        {
            get => _secondValue;
            set => SetProperty(ref _secondValue, value);
        }

        private string _operationValue;
        public string OperationValue
        {
            get => _operationValue;
            set => SetProperty(ref _operationValue, value);
        }

        private string _answerValue;
        public string AnswerValue
        {
            get => _answerValue;
            set => SetProperty(ref _answerValue, value);
        }


        private DelegateCommand _calcCommand;
        public DelegateCommand CalcCommand
        {
            get
            {
                if (_calcCommand == null)
                {
                    _calcCommand = new DelegateCommand(CalcAnswer);
                }
                return _calcCommand;
            }
        }

        private void CalcAnswer()
        {
            if (!IsValid())
            {
                _userDialogs.Alert("Fill all fields!");
                return;
            }

            try
            {
                AnswerValue = DetectOperationAndCalc(Convert.ToInt32(_firstValue), Convert.ToInt32(_secondValue), _operationValue).ToString();
            }
            catch(Exception e)
            {
                _userDialogs.Alert(e.Message);
            }
        }

        private int DetectOperationAndCalc(int v1, int v2, string operationValue)
        {
            int result;
            switch (operationValue)
            {
                case "+":
                    result = v1 + v2;
                    break;
                case "-":
                    result = v1 - v2;
                    break;
                case "/":
                    try
                    {
                        result = v1 / v2;
                    }
                    catch(Exception e)
                    {
                        throw e;
                    }
                    break;
                case "*":
                    result = v1 * v2;
                    break;
                default:
                    throw new InvalidOperationException("Invalid operation!");
            }
            return result;
        }

        private bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(_firstValue) && !string.IsNullOrWhiteSpace(_secondValue) && !string.IsNullOrWhiteSpace(_operationValue);
        }
    }
}
