using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ACTAP.Item_Scripts.Traps 
{
   class DarkSoulsTrap : MonoBehaviour
    {
        System.Random rand = new System.Random();
        List<string> trapMessages = new List<string>
        {
            "BOO!",
            "It would be a shame if you couldn't see right now.",
            "NintenDogs assist trophy be like",
            "MarioKart Bloopers be like",
            "Fort Nite Fort Nite Fort Nite Fort Nite Fort Nite Fort Nite Fort Nite Fort Nite Fort Nite Fort Nite Fort Nite Fort Nite",
            "Too Many Cooks",
            "Starring: Kril.\nFeaturing: Chitan as The Honorable Knight and Roland as The Venture Crabitalist.\nGuest Starring: Firth, Nemma, and Shromp.",
            "Aggro Crab Presents: Another Crab's Treasure",
            "Aggro Crab Presents: Going Und- wait thats the wrong game...",
            "I'm running out of funny things to write",
            "I miss my wife Kril. I miss her a lot.",
            "https://www.youtube.com/watch?v=E4WlUXrJgy4",
            "You Died.",
            "Something wicked this way comes."
        };

        public void ActivateTrap()
        {
            Debug.Log("Entered Method");
            StartCoroutine(TextRoutine(3));
        }

        public IEnumerator TextRoutine(int messageCount)
        {
            Debug.Log("Entered Coroutine");
            for (int i = 0; i < messageCount; i++)
            {
                string stringToUse = trapMessages[rand.Next(trapMessages.Count)];

                GUIManager.instance.PlayDarkSoulsText(stringToUse, "YouDied");
                yield return new WaitForSeconds(8f);
            }
        }
    }
}
