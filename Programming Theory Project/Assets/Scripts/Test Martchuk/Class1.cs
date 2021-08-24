using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Class1:MonoBehaviour
{
	public int totalChange;
	public int[] possibleChange;

    public int countChangeTen = 0;
    public int countChangeFive = 0;
    public int countChangeTwo = 0;
    int changeDelta;

   public void InputData()
    {
        totalChange = Random.Range(2, 100);
        changeDelta = totalChange;
        countChangeTen = 0;
        countChangeFive = 0;
        countChangeTwo = 0;
}
    public void Calculation()
    {        
        for (int i = 0; i < possibleChange.Length; i++)
        {
            if (totalChange % 2 == 0)
            {
                if (possibleChange[i] / 10 == 1)
                {
                    while (changeDelta / possibleChange[i] >= 1 && changeDelta >= possibleChange[i])
                    {
                        countChangeTen++;
                        changeDelta -= possibleChange[i];
                    }
                }
                else if (possibleChange[i] / 2 == 1)
                {
                    while (changeDelta != 0)
                    {
                        countChangeTwo++;
                        changeDelta -= possibleChange[i];
                    }
                }
            }
            else if (totalChange % 2 != 0)
            {
                if (possibleChange[i] / 10 == 1)
                {
                    while (changeDelta / possibleChange[i] >= 1 && changeDelta >= 2 * possibleChange[i])
                    {
                        countChangeTen++;
                        changeDelta -= possibleChange[i];                        
                    }
                    if (changeDelta / 5 >= 3)
                    {
                        countChangeTen++;
                        changeDelta -= possibleChange[i];
                    }
                }
                else if (possibleChange[i] / 5 == 1)
                {
                    while (changeDelta / possibleChange[i] >= 1 && changeDelta % 2 != 0)
                    {
                        countChangeFive++;
                        changeDelta -= possibleChange[i];
                        Debug.Log("A-HA!!");
                    }
                }
                else if (possibleChange[i] / 2 == 1)
                {
                    while (changeDelta != 0)
                    {
                        countChangeTwo++;
                        changeDelta -= possibleChange[i];
                    }
                }
            }
        }
    }
	
}
