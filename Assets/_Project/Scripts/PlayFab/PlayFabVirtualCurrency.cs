using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace Web3_Skyrim
{
    public class PlayFabVirtualCurrency
    {
        public event Action<int> OnGetBalanceSuccess;
        
        private const string CrystalCurrencyCode = "CR";

        public void GetBalance()
        {
            PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnSuccess, OnError);
        }

        // TODO Do this with PlayStream event
        public void SubstractAmount(int amount)
        {
            var request = new SubtractUserVirtualCurrencyRequest()
            {
                VirtualCurrency = CrystalCurrencyCode,
                Amount = amount
            };
            
            PlayFabClientAPI.SubtractUserVirtualCurrency(request, 
                result =>
                {
                    // We assume this will work well. You should handle this with some callbacks like we do in GetBalance()
                    Debug.Log(amount + " CR substracted!");
                }, error =>
                {
                    Debug.Log("Crystal substraction failed");
                });
        }   

        private void OnSuccess(GetUserInventoryResult result)
        {
            int coins = result.VirtualCurrency[CrystalCurrencyCode];
            OnGetBalanceSuccess?.Invoke(coins);
        }
        
        private void OnError(PlayFabError error)
        {
            Debug.Log(error);
        }
    }   
}
