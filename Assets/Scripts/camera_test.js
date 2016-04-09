   ////////////////////////////////////////////////////////////////////

     //IMPORTANT! Tag ALL players with "Player" so they are recognized.//
     ////////////////////////////////////////////////////////////////////  


private var isOrthographic : boolean;
var targets : GameObject[];
var currentDistance : float;
var largestDistance : float;
var theCamera : Camera;
var height : float = 5.0;
var avgDistance;
var distance = 0.0;                    // Default Distance 
var speed = 1;
var offset : float;

//========================================

function Start(){

     targets = GameObject.FindGameObjectsWithTag("Player"); 
     if(theCamera) isOrthographic = theCamera.orthographic;


}

function OnGUI(){

     GUILayout.Label("largest distance is = " + largestDistance.ToString());
     GUILayout.Label("height = " + height.ToString());
     GUILayout.Label("number of players = " + targets.length.ToString());


}

function LateUpdate () 

{

     targets = GameObject.FindGameObjectsWithTag("Player"); 
     if (!GameObject.FindWithTag("Player"))
     return;
     var sum = Vector3(0,0,0);
     for (var n = 0; n < targets.length ; n++){
         sum += targets[n].transform.position;
     }
       var avgDistance = sum / targets.length;


  //    Debug.Log(avgDistance);

       var largestDifference = returnLargestDifference();
       height = Mathf.Lerp(height,largestDifference,Time.deltaTime * speed);
         if(isOrthographic){
             theCamera.transform.position.x = avgDistance.x ;
             theCamera.orthographicSize = largestDifference;
             theCamera.transform.position.y = height;
             theCamera.transform.LookAt(avgDistance);
         } else {
             theCamera.transform.position.x = avgDistance.x ;
             theCamera.transform.position.z = avgDistance.z - distance + largestDifference;
             theCamera.transform.position.y = height;
             theCamera.transform.LookAt(avgDistance);
         }


}

function returnLargestDifference(){

     currentDistance = 0.0;
     largestDistance = 0.0;
     for(var i = 0; i < targets.length; i++){
         for(var j = 0; j <  targets.length; j++){
             currentDistance = Vector3.Distance(targets[i].transform.position,targets[j].transform.position);
             if(currentDistance > largestDistance){
                 largestDistance = currentDistance;
             }
         }
     }
     return largestDistance;


}