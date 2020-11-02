<?php
    $methode=$_SERVER["REQUEST_METHOD"];
    if ($methode=="GET")
	    $param=$_GET;
    else 
        $param=$_POST;
    include 'EcrireXML3.php';

    if (isset($_POST['type'])){
        $choix=$_POST['type'];
    }
    else {
        echo "Aucune reponse n'a été trouvée, 
        impossible de créer le fichier XML.";
        die;
    }
    
    $user="root";		// Login
	$pwd="";			// pwd
	$bdd="QCM";			// BD MySQL
	$hote="localhost";	// Serveur
    $dsn = "mysql:host=$hote;dbname=$bdd"; 
    $laquestion= $clef[$choix];
    $sql = "SELECT * FROM questions WHERE cle = $laquestion";
    
    // 4. Connexion au serveur et a la BDD
    
    try
    {
        $pdo = new PDO($dsn, $user, $pwd);
        $stmt = $pdo->query($sql);
        
        if($stmt === false){
         die("Erreur");
        }
        
    }
    catch (PDOException $e)
    {
        echo $e->getMessage();
    }

    $lesQuestions;
    while($row = $stmt->fetch(PDO::FETCH_ASSOC)) {
        
        $lesQuestions[$row['rang']] = array(
            'typeQ' => $row['typeQ'],
            'name' => $row['name'],
            'text' => $row['text'],
            'defaut' => $row['defaut'],
            'reponses' => array()
        );

        for($i=1;$i<=5;$i++){
            $rep = "reponse" . $i;
            if ($row[$rep] != "vide"){
                $lesQuestions[$row['rang']]['reponses']['reponse'.$i] = $row['reponse'.$i];
            }
        }
    }
     
    $cnx=null;

    $xml = new DOMDocument('1.0', 'iso-8859-1');
    $xml->xmlStandalone = true;
    $xml->formatOutput = true;
    $xml->preserveWhiteSpace  = false;

    // création d'une balise questionnaire et ajout des attributs
    $xml_questionnaire = $xml->createElement('questionnaire');
    $xml_questionnaire->setAttribute("cle", $clef[$choix]);
    $xml_questionnaire->setAttribute("name", $name[$choix]);
    $xml_questionnaire->setAttribute("displayName", $dName[$choix]);

    $xml_description = $xml->createElement('description', $desc[$choix]);
    $xml_questionnaire->appendChild($xml_description);

    // création des questions
    foreach ($lesQuestions as $uneQuestion) {
        $xml_question = $xml->createElement('question');
        $xml_question->setAttribute("type", $uneQuestion['typeQ']);
        $xml_question->setAttribute("name", $uneQuestion['name']);
        
        $xml_text = $xml->createElement('text', $uneQuestion['text']);
        $xml_question->appendChild($xml_text);

        $xml_reponses = $xml->createElement('reponses');
        
        for($i=1;$i<=count($uneQuestion)-1;$i++){
            $rep = "reponse" . $i;
            $xml_reponse = $xml->createElement('reponse', $uneQuestion['reponses'][$rep]);

            if (strpos($rep, $uneQuestion['defaut'])) {
                $xml_reponse->setAttribute("default", 'true');
            } else {
                $xml_reponse->setAttribute("default", 'false');
            }

            $xml_reponses->appendChild($xml_reponse);
        }

        $xml_question->appendChild($xml_reponses);
        $xml_questionnaire->appendChild($xml_question);
    }

    $xml->appendChild($xml_questionnaire);

    $xml->loadXML($xml->saveXML());
    $larequete = $name[$choix] . $clef[$choix];
    $xml->save($larequete .'.XML');

?>