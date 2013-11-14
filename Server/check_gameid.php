<?php
$dat = "Games/".$gameid."/GameInfo.dat";
if (!file_exists($dat) ){
echo "FEHLER\r\nSpielID existiert nicht!\r\n";
exit;
}
?>