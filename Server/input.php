<?php
if (isset($_GET)){
reset($_GET);
foreach($_GET as $key=>$element){
${"$key"} = $element;
}}

if (isset($_POST)){
reset($_POST);
foreach($_POST as $key=>$element){
${"$key"} = $element;
}}
?>