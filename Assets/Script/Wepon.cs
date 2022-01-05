using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wepon : MonoBehaviour
{
   public GameObject bullet;
   public Boolean isActivity; 
   private void Update()
   {
      if (Input.GetButtonDown("Fire1") && isActivity)
      {
         var b = Instantiate(bullet);
         b.transform.position = this.gameObject.transform.position;
      }
   }
}
