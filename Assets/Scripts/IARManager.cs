using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Play.Core;
using Google.Play.Review;

public class IARManager : MonoBehaviour
{
    private ReviewManager reviewManager;
    private PlayReviewInfo playReviewInfo;

    private void Start()
    {
        var count = PlayerPrefs.GetInt("Reviewed");

        if (count > 10)
        {

        }
        else if (count == 0)
        {
            PlayerPrefs.SetInt("Reviewed", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Reviewed", count + 1);
        }

        if (count == 2 || count == 10)
        {
            StartCoroutine(RequestReviews());
        }
    }

    IEnumerator RequestReviews()
    {
        reviewManager = new ReviewManager();

        var requestFlowOperation = reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
        playReviewInfo = requestFlowOperation.GetResult();

        var launchFlowOperation = reviewManager.LaunchReviewFlow(playReviewInfo);
        yield return launchFlowOperation;
        playReviewInfo = null; // Reset the object
        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
        // The flow has finished. The API does not indicate whether the user
        // reviewed or not, or even whether the review dialog was shown. Thus, no
        // matter the result, we continue our app flow.
    }
}
