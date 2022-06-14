using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeDesignPattern : MonoBehaviour
{
    /*
    [SerializeField]IAbility currentAbility =
    new SequenceComposite(
        new IAbility[]
        {
            new HealAbility(),
            new RageAbility(),
            new blablabla()
        }
    );

*/



    public interface IAbility
    {
        void Use(GameObject currentGameObject);
    }

    public class SequenceComposite : IAbility
    {
        private IAbility[] children;    //you can make this an array or List

        public SequenceComposite(IAbility[] children) // we create a constructor which takes in the children array
        {
            this.children = children;
        }

        public void Use(GameObject currentGameObject)
        {
            foreach (var child in children) 
            {
                child.Use(currentGameObject);
            }
        }
    }
}
