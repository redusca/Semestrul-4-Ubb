<?php
$passwords = [
    'Codrin' => 'ode432',
    'Bogda' => 'bogdamusic',
];

foreach ($passwords as $user => $plain) {
    $hash = password_hash($plain, PASSWORD_DEFAULT);
    echo "('$user', '$hash'),\n";
}