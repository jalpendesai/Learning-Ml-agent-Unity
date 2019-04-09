# Learn-Ml-agent-Unity

# Create virtual environment in ANACONDA
```
conda create -n env_name python=3.6 anaconda
```

# Clone Ml-agent and run following command
```
git clone https://github.com/Unity-Technologies/ml-agents.git  
cd ml-agents  
cd ml-agents  
pip3 install -e.
```


# For Training Agent
Navigate to the ml-agent toolkit installation folder and run following command
```
mlagents-learn ../config/trainer_config.yaml --run-id=RollerBall-1 --train
```

# For Summary graph
```
tensorboard --logdir=summaries
```
