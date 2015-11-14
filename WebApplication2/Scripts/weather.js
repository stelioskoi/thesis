function WeatherCheckBoxMaxMin(oBtn, sContentID)
{
	var oContent = dnn.dom.getById(sContentID);
	if (oContent != null)
	{
		if (oContent.style.display == 'none')
		{
			oContent.style.display = '';
			return true;
		}
		else
		{
			oContent.style.display = 'none';
			return false;
		}
	}
	//failed so do postback
}

function WeatherRadioMaxMin(oBtn, sContentID)
{
	var oContent = dnn.dom.getById(sContentID);
	if (oContent != null)
	{
		if (oContent.style.display == 'none')
		{
			oContent.style.display = '';
			return true;
		}
		else
		{
			oContent.style.display = 'none';
			return false;
		}
	}
	//failed so do postback
}