using Cooking.Control;
using Cooking.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking.World
{
    public class CookingItem : AbstractCookingItem, IInteractable
    {
        public bool IsInteractable { get; private set; } = true;

        public void Interact()
        {
            if (IsInteractable)
            {
                CookingController.Instance.HoldItem(this);
            }
        }
    }
}
