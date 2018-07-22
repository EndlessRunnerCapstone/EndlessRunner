using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Assets.Scripts.MatchMaker;

public class Test {

    [Test]
    public void MatchMaker_SetsAutoJoinLobbyToFalse() {
        // Use the Assert class to test conditions.
        var nal = new NetworkAbstractionLayerSub();
        
        MatchMaker matchMaker = new MatchMaker(nal);

        matchMaker.Awake();

        Assert.IsFalse(nal.autoJoinLobby);
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator TestWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }
}
