﻿using UnityEngine;
using GameProtocol.PAY;

#if UNITY_IAP
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
#endif

// Placing the Purchaser class in the CompleteProject namespace allows it to interact with ScoreManager, 
// one of the existing Survival Shooter scripts.
namespace View.Home.IAP
{
    // Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
    public class Purchaser : MonoBehaviour
#if UNITY_IAP
        , IStoreListener
#endif
    {
        #if UNITY_IAP
        private static IStoreController m_StoreController;          // The Unity Purchasing system.
        private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

        // Product identifiers for all products capable of being purchased: 
        // "convenience" general identifiers for use with Purchasing, and their store-specific identifier 
        // counterparts for use with and outside of Unity Purchasing. Define store-specific identifiers 
        // also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

        // General product identifiers for the consumable, non-consumable, and subscription products.
        // Use these handles in the code to reference which product to purchase. Also use these values 
        // when defining the Product Identifiers on the store. Except, for illustration purposes, the 
        // kProductIDSubscription - it has custom Apple and Google identifiers. We declare their store-
        // specific mapping to Unity Purchasing's AddProduct, below.
        Package[] packages;
        public static string PRODUCT_099 = "id_099";
        public static string PRODUCT_199 = "id_199";
        public static string kProductIDSubscription = "subscription";

        // Apple App Store-specific product identifier for the subscription product.
        // private static string kProductNameAppleSubscription = "com.unity3d.subscription.new";

        // Google Play Store-specific product identifier subscription product.
        //private static string kProductNameGooglePlaySubscription = "com.unity3d.subscription.original";

        void Awake()
        {
            //if (m_StoreController == null)
            //{
            //    // Begin to configure our connection to Purchasing
            //    InitializePurchasing();
            //}
        }
        void Start()
        {
            InitIAPView();
            // If we haven't set up the Unity Purchasing reference

        }

        IAPViewScritp IapView;
        private void InitIAPView()
        {
            IapView = GetComponent<IAPViewScritp>();
        }

        public void InitializePurchasing(Package[] packages)
        {
            this.packages = packages;
            // If we have already connected to Purchasing ...
            if (IsInitialized())
            {
                // ... we are done here.
                return;
            }

            // Create a builder, first passing in a suite of Unity provided stores.
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            for(int i = 0; i < packages.Length; i++)
            {
                builder.AddProduct(this.packages[i].PackageName, ProductType.Consumable, new IDs()
                {
                    { this.packages[i].ProductId, GooglePlay.Name },
                    { this.packages[i].ProductId, AppleAppStore.Name },
                });
            }

            //// Add a product to sell / restore by way of its identifier, associating the general identifier
            //// with its store-specific identifiers.
            //builder.AddProduct(PRODUCT_099, ProductType.Consumable, new IDs()
            //{
            //    { PRODUCT_099, GooglePlay.Name },
            //    { PRODUCT_099, AppleAppStore.Name },
            //});
            //// Continue adding the non-consumable product.
            //builder.AddProduct(PRODUCT_199, ProductType.Consumable, new IDs()
            //{
            //     { PRODUCT_199, GooglePlay.Name },
            //    { PRODUCT_199, AppleAppStore.Name },
            //});

            // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
            // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
            UnityPurchasing.Initialize(this, builder);
        }


        private bool IsInitialized()
        {
            // Only say we are initialized if both the Purchasing references are set.
            return m_StoreController != null && m_StoreExtensionProvider != null;
        }


        public void BuyProduct099()
        {
            // Buy the consumable product using its general identifier. Expect a response either 
            // through ProcessPurchase or OnPurchaseFailed asynchronously.
            Debug.Log("BuyProduct099");
            BuyProductID(PRODUCT_099);
        }


        public void BuyProduct199()
        {
            // Buy the non-consumable product using its general identifier. Expect a response either 
            // through ProcessPurchase or OnPurchaseFailed asynchronously.
            Debug.Log("BuyProduct199");
            BuyProductID(PRODUCT_199);
        }

        private CrossPlatformValidator validator;


        public void BuyProductID(string productId)
        {
            if (IapView == null) InitIAPView();
            // If Purchasing has been initialized ...
            if (IsInitialized())
            {
                // ... look up the Product reference with the general product identifier and the Purchasing 
                // system's products collection.
                Product product = m_StoreController.products.WithID(productId);

                // If the look up found a product for this device's store and that product is ready to be sold ... 
                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                    // asynchronously.
                    m_StoreController.InitiatePurchase(product);
                    //try
                    //{
                    //    var unifiedReceipt = JsonUtility.FromJson<UnifiedReceipt>(product.receipt);
                    //    if (unifiedReceipt != null && !string.IsNullOrEmpty(unifiedReceipt.Payload))
                    //    {
                    //        var purchaseReceipt = JsonUtility.FromJson<UnityChannelPurchaseReceipt>(unifiedReceipt.Payload);
                    //        //string str =
                    //        //string.Format("UnityChannel receipt: storeSpecificId = {0}, transactionId = {1}, signature = {2}",
                    //        //    purchaseReceipt.storeSpecificId, purchaseReceipt.transactionId, purchaseReceipt.signature);
                    //        //DialogEx.DialogExViewScript.Instance.ShowDialog("IAP: " + str);
                    //    }
                    //}
                    //catch
                    //{

                    //}

                }
                // Otherwise ...
                else
                {
                    // ... report the product look-up failure situation  
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            // Otherwise ...
            else
            {
                // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
                // retrying initiailization.
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }


        // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
        // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
        public void RestorePurchases()
        {
            // If Purchasing has not yet been set up ...
            if (!IsInitialized())
            {
                // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
                Debug.Log("RestorePurchases FAIL. Not initialized.");
                return;
            }

            // If we are running on an Apple device ... 
            if (Application.platform == RuntimePlatform.IPhonePlayer ||
                Application.platform == RuntimePlatform.OSXPlayer)
            {
                // ... begin restoring purchases
                Debug.Log("RestorePurchases started ...");

                // Fetch the Apple store-specific subsystem.
                var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
                // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
                // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
                apple.RestoreTransactions((result) =>
                {
                    // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                    // no purchases are available to be restored.
                    Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
                });
            }
            // Otherwise ...
            else
            {
                // We are not running on an Apple device. No work is necessary to restore purchases.
                Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
            }
        }


        //  
        // --- IStoreListener
        //

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            // Purchasing has succeeded initializing. Collect our Purchasing references.
            Debug.Log("OnInitialized: PASS");

            // Overall Purchasing system, configured with products for this application.
            m_StoreController = controller;
            // Store specific subsystem, for accessing device-specific store features.
            m_StoreExtensionProvider = extensions;
        }


        public void OnInitializeFailed(InitializationFailureReason error)
        {
            // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
            Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
        }


        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
#if UNITY_ANDROID
                Receipt receipt = JObject.Parse(args.purchasedProduct.receipt).ToObject<Receipt>();
                Payload payload = JObject.Parse(receipt.Payload).ToObject<Payload>();
                if (IapView) IapView.PurchaserSuccessGoogle(payload.Json, payload.Signature);
            //if (IAPViewScritp.Instance) ChargingBoxViewScript.Instance.RequestGoogleBilling(payload.Json, payload.Signature);
#elif UNITY_IOS
				string packageId = args.purchasedProduct.definition.id;
				string receiptStr = args.purchasedProduct.receipt;
				Receipt receipt = JObject.Parse(receiptStr).ToObject<Receipt>();
				string transactionId = receipt.TransactionID;
            
                if (IapView) IapView.PurchaserSuccessApple(packageId, transactionId, receiptStr);
				//if (ChargingBoxViewScript.Instance) ChargingBoxViewScript.Instance.RequestAppleBilling(packageId, transactionId, receiptStr);

#endif
            return new PurchaseProcessingResult();
        }


        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
            // this reason with the user to guide their troubleshooting actions.
            Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
        }

#region IAP OBJECT
        public class Receipt
        {
            public string Store;
            public string TransactionID;
            public string Payload;

            public override string ToString()
            {
                return string.Format("Store: \"{0}\", TransactionID: \"{1}\", Payload: \"{2}\"", Store, TransactionID, Payload);
            }
        }

        public class Payload
        {
            public string Json;
            public string Signature;

            public override string ToString()
            {
                return string.Format("Payload: {Json: \"{0}\", Signature: \"{1}\"}", Json, Signature);
            }
        }

        public class Json
        {
            public string OrderId;
            public string PackageName;
            public string ProductId;
            public string PurchaseTime;
            public string PurchaseState;
            public string PurchaseToken;

            public override string ToString()
            {
                return string.Format("Json: {OrderId: \"{0}\", PackageName: \"{1}\", ProductId: \"{2}\", PurchaseTime: \"{3}\", PurchaseState: \"{4}\", PurchaseToken: \"{5}\"}", OrderId, PackageName, ProductId, PurchaseTime, PurchaseState, PurchaseToken);
            }
        }

#endregion
#endif
    }
}