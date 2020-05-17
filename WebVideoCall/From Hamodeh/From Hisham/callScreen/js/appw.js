// JavaScript source code
// replace these values with those generated in your TokBox Account
var apiKey = "46530282";
var sessionId = "2_MX40NjUzMDI4Mn5-MTU4MzUwNDgxNTIyNX4yS3A0Nk1OZno4YVlUQ0pGWWFxUmxFbkl-fg";
var token = "T1==cGFydG5lcl9pZD00NjUzMDI4MiZzaWc9NGM4NjYxMzY2MDdhOTZhODAwZjU4MjE4MDI1YzMyYmRlZDFjYmYzMzpzZXNzaW9uX2lkPTJfTVg0ME5qVXpNREk0TW41LU1UVTRNelV3TkRneE5USXlOWDR5UzNBME5rMU9abm80WVZsVVEwcEdXV0Z4VW14RmJrbC1mZyZjcmVhdGVfdGltZT0xNTgzNTA0ODM3Jm5vbmNlPTAuMjE1Njg3NTgzNTc1ODIxODUmcm9sZT1wdWJsaXNoZXImZXhwaXJlX3RpbWU9MTU4NjA5MzIzNiZpbml0aWFsX2xheW91dF9jbGFzc19saXN0PQ==";
//658b7502148a073ec75b45a33c1b65b673aca7b1
var callclicked = false;
var UserKey = false;
var UserData;
function Call() {
    $('.call').html('جاري الاتصال');
    if (callclicked == false) {
        var ApplicationID = '';

        //var UserData;
        var ref = firebase.database().ref();
        firebase.database().ref('/Users').once('value').then(function (snapshot) {

            var ObjUsers = JSON.parse(JSON.stringify(snapshot.val(), null, 2));
            for (var key in ObjUsers) {
                //if ((ObjUsers[key].IsOnline == 1) && (ObjUsers[key].IsBusy == 0) && (ObjUsers[key].Country == parseInt( $("#optSelectedCountry").val()))) {

                if ((ObjUsers[key].IsOnline == 1) && (ObjUsers[key].IsBusy == 0) && (ObjUsers[key].Country == 1)) {
                    ApplicationID = ObjUsers[key].ApplicationID
                    UserKey = key;
                    UserData = {
                        ApplicationID: ObjUsers[key].ApplicationID,
                        Country: ObjUsers[key].Country,
                        SessionId: ObjUsers[key].SessionId,
                        IsBusy: 1,
                        IsOnline: ObjUsers[key].IsOnline,
                        Token: ObjUsers[key].Token,
                        Name: ObjUsers[key].Name,
                        PhoneNumber: ObjUsers[key].PhoneNumber,
                        password: ObjUsers[key].PhoneNumber,
                    };
                    break;
                }
            }
            if (ApplicationID != '') {//publish()


                // Get a key for a new Post.
                var newPostKey = firebase.database().ref().child('Users').push().key;

                // Write the new post's data simultaneously in the posts list and the user's post list.
                var updates = {};
                updates['/Users/' + UserKey] = UserData;
                firebase.database().ref().update(updates);

                initializeSession()
                callclicked = true;
                var starCountRefs = firebase.database().ref('Users/' + UserKey);
                starCountRefs.on('value', function (snapshot) {
                    var ObjUsers = JSON.parse(JSON.stringify(snapshot.val(), null, 2));
                    if (ObjUsers.IsBusy == 0) {//User Canceled
                        window.location.href = window.location.href
                    }
                });
            }
            else {
                alert('جميع موظفين مركز الإتصال مشغولون حاليا')
                window.location.href = window.location.href;
                callclicked = false;
            }
        });
    }
    else {
        alert('يوجد اتصال جاري حاليا');
    }
}
function End() {
    var updates = {};
    UserData.IsBusy = 0;
    updates['/Users/' + UserKey] = UserData;
    firebase.database().ref().update(updates);
    location.reload();
}

// Handling all of our errors here by alerting them
function handleError(error) {
    if (error) {
        alert(error.message);
    }
}
// (optional) add server code here
//initializeSession();
function initializeSession() {
    var session = OT.initSession(apiKey, sessionId);

    // Subscribe to a newly created stream
    session.on('streamCreated', function (event) {
        session.subscribe(event.stream, 'subscriber', {
            insertMode: 'append',
            width: '100%',
            height: '100%'
        }, handleError);
    });

    // Create a publisher
    var publisher = OT.initPublisher('publisher', {
        insertMode: 'append',
        width: '100%',
        height: '100%'
    }, handleError);

    // Connect to the session
    session.connect(token, function (error) {
        // If the connection is successful, initialize a publisher and publish to the session
        if (error) {
            handleError(error);
        } else {
            session.publish(publisher, handleError);
        }
    });
}