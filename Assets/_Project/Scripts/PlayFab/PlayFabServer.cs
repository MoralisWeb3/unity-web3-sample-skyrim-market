using System.Collections.Generic;
using MoralisUnity;
using Pixelplacement;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

// PlayFab Client/Server specifications
using UpdateUserDataRequest = PlayFab.ServerModels.UpdateUserDataRequest;
using UserDataPermission = PlayFab.ServerModels.UserDataPermission;

namespace Web3_Skyrim
{
    public class PlayFabServer : Singleton<PlayFabServer>
    {
        [HideInInspector] public string playerId;
        [HideInInspector] public int currentCrystalBalance;
        
        //PlayFab Classes
        private PlayFabVirtualCurrency _playFabVirtualCurrency;
    
        private void OnEnable()
        {
            Authenticating.OnSuccess += SetPlayFabUserData;

            _playFabVirtualCurrency = new PlayFabVirtualCurrency();
            _playFabVirtualCurrency.OnGetBalanceSuccess += OnGetBalanceSuccessHandler;
        }

        private void OnDisable()
        {
            Authenticating.OnSuccess -= SetPlayFabUserData;
            _playFabVirtualCurrency.OnGetBalanceSuccess -= OnGetBalanceSuccessHandler;
        }


        #region EVENT_HANDLERS

        private void SetPlayFabUserData(LoginResult loginResult)
        {
            playerId = loginResult.PlayFabId;

            var walletAddress = Web3Tools.GetWalletAddress();
            var chainId = Moralis.CurrentChain.ChainId;
        
            PlayFabServerAPI.UpdateUserReadOnlyData(new UpdateUserDataRequest() {
                    PlayFabId = playerId,
                    Data = new Dictionary<string, string>() {
                        {"WalletAddress", walletAddress.ToString()},
                        {"ChainId", chainId.ToString()}
                    },
                    Permission = UserDataPermission.Public
                },
                result => Debug.Log("Set read-only user data successful"),
                error => {
                    Debug.Log("Got error updating read-only user data:");
                    Debug.Log(error.GenerateErrorReport());
                });
        }
        
        private void OnGetBalanceSuccessHandler(int currencyBalance)
        {
            currentCrystalBalance = currencyBalance;
        }

        #endregion
    }
}
