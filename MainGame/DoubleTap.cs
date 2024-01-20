using System;

var tapping : boolean;
var LastTap : float;
var tapTime : float;


function Update()
{
    if (Input.GetKeyDown(KeyCode.LeftShift))
    {
        if (!tapping)
        {
            tapping = true;
            SingleTap();
        }
        if ((Time.time - LastTap) < tapTime)
        {
            Debug.Log("DoubleTap");
            tapping = false;
        }
        LastTap = Time.time;
    }
}

public function SingleTap()
{
    yield WaitForSeconds(tapTime);
    if (tapping)
    {
        Debug.Log("SingleTap");
        tapping = false;
    }
}