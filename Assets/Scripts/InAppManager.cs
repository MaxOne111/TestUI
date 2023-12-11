using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace IAP
{
    public class InAppManager : MonoBehaviour, IStoreListener
    {
        private static IStoreController m_StoreController;
        private static IExtensionProvider m_StoreExtensionProvider;
        
        private static string _Epic_Chest = "epicChest";
        private static string _Lucky_Chest = "luckyChest";
        

        void Start()
        {
            if (m_StoreController == null)
            {
                InitializePurchasing();
            }
        }

        public void InitializePurchasing()
        {
            if (IsInitialized())
            {
                return;
            }

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            builder.AddProduct(_Epic_Chest, ProductType.Consumable);
            builder.AddProduct(_Lucky_Chest, ProductType.Consumable);

            UnityPurchasing.Initialize(this, builder);
        }

        private bool IsInitialized()
        {
            return m_StoreController != null && m_StoreExtensionProvider != null;
        }

        public void BuyProductID(string productId)
        {
            try
            {
                if (IsInitialized())
                {
                    Product product = m_StoreController.products.WithID(productId);

                    if (product != null && product.availableToPurchase)
                    {
                        Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
                        m_StoreController.InitiatePurchase(product);
                    }
                    else
                    {
                        Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                    }
                }
                else
                {
                    Debug.Log("BuyProductID FAIL. Not initialized.");
                }
            }
            catch (Exception e)
            {
                Debug.Log("BuyProductID: FAIL. Exception during purchase. " + e);
            }
        }

        public void RestorePurchases()
        {
            if (!IsInitialized())
            {
                Debug.Log("RestorePurchases FAIL. Not initialized.");
                return;
            }

            if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
            {
                Debug.Log("RestorePurchases started ...");

                var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
                apple.RestoreTransactions((result) =>
                    {
                        Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
                    });
            }
            else
            {
                Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
            }
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("OnInitialized: Completed!");

            m_StoreController = controller;
            m_StoreExtensionProvider = extensions;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            throw new NotImplementedException();
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            if (String.Equals(args.purchasedProduct.definition.id, _Epic_Chest, StringComparison.Ordinal))
            {
                PlayerData.RecieveTickets(500);
            }
            else if (String.Equals(args.purchasedProduct.definition.id, _Lucky_Chest, StringComparison.Ordinal))
            {
                PlayerData.RecieveTickets(1200);
            }

            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
        }
    }
}