﻿using Microsoft.AspNetCore.Components.Forms;

namespace CustomInputAndValidation.CustomInput
{
    public class CustomInputSelect<TValue> : InputSelect<TValue>
    { 
        public string ErrorMess {  get; set; }

        protected override bool TryParseValueFromString(string value, out TValue result,
            out string validationErrorMessage)
        {
            if (typeof(TValue) == typeof(int))
            {
                if (int.TryParse(value, out var resultInt))
                {
                    result = (TValue)(object)resultInt;
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
                    result = default;
                    validationErrorMessage =$"The selected value {value} is not a valid number.";
                    //validationErrorMessage = ErrorMess;
                    return false;
                }
            }
            else return base.TryParseValueFromString(value, out result, out validationErrorMessage);
        }
    }
}
