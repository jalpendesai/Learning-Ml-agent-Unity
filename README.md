# Learn-Ml-agent-Unity

# Clone Ml-agent and run following command
pip3 install -e.

Navigate to the ml-agent toolkit installation folder and run following command

# For Training Agent
mlagents-learn ../config/trainer_config.yaml --run-id=RollerBall-1 --train

# For Summary graph
tensorboard --logdir=summaries
