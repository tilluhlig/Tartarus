<?php
include('input.php');
include('check_input.php');

// name, passwd
$dat = "User/".$name.".dat";
@$array = file($dat);
if (count($array)<4){
echo "FEHLER\r\nFehlerhafte User-Datei!\r\n";
exit;
}

@$array = file($dat);
for ($i=0;$i<count($array);$i++){
if (rtrim($array[$i])=="<games>"){
for ($b=$i+1;$b<count($array);$b++){
if (rtrim($array[$b])=="<endgames>")break;
echo $array[$b];
}
break;
}}


?>