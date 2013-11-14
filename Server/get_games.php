<?php
include('input.php');
include('check_input.php');
// name, passwd

$dat = "Spiele.txt";
if (!file_exists($dat) ){
echo "FEHLER\r\nSpiele existieren nicht!\r\n";
exit;
}

@$m_array = file($dat);
for ($i=0;$i<count($m_array);$i++){
echo $m_array[$i];
}
?>