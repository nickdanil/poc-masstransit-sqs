var AWS = require('aws-sdk'),
	awsCredentials = {
		"accessKeyId": "foo",
		"secretAccessKey": "bar",
		"region": "eu-west-1"
	},
	sqsQueueUrl = 'http://localhost:4576/queue/carina-sending-email',
	sqs;

new AWS.Config(awsCredentials);
AWS.config.update({ region: 'eu-west-1' });
// Instantiate SQS client
sqs = new AWS.SQS();

var msg = {
	// "messageId": "4d550000-351a-00ff-d879-08d6da98fe9c",
	// "conversationId": "4d550000-351a-00ff-8650-08d6da98feab",
	// "sourceAddress": "amazonsqs://localhost/NSBWS27_dotnet_bus_jikoyybideyx9kd4bdmpig8iba?durable=false&autodelete=true",
	// "destinationAddress": "amazonsqs://localhost/masstran-Mess",
	"messageType": [
		"urn:message:masstran:Mess" //required
	],
	"message": {
		"msg": "Outer message" //required
	},
	//"sentTime": "2019-05-17T07:26:36.2143909Z",
	//"headers": {},
	// "host": {
	//     "machineName": "NSB-WS-27",
	//     "processName": "dotnet",
	//     "processId": 12032,
	//     "assembly": "masstran",
	//     "assemblyVersion": "1.0.0.0",
	//     "frameworkVersion": "4.0.30319.42000",
	//     "massTransitVersion": "5.3.2.0",
	//     "operatingSystemVersion": "Microsoft Windows NT 6.2.9200.0"
	// }
}
var params = {
	MessageBody: JSON.stringify(msg),
	QueueUrl: sqsQueueUrl,
	DelaySeconds: 0
};
sqs.sendMessage(params, function (err, data) {
	if (err) console.log(err, err.stack); // an error occurred
	else console.log(data);           // successful response
});