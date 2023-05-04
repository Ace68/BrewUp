In order to start the application in a local environment, you need to have helm installed and at least a minikube instance running.
After that you could run the following:
```
helm install brewup-monolithic -f values.yaml --namespace=brewup-monolithic --create-namespace . 
```