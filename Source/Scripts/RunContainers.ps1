docker stop convention.adminpanel convention.website

docker rm convention.adminpanel convention.website

cd ..\Convention.AdminPanel
npm run build

cd ..\Convention.Website
npm run build

cd ..\Scripts

docker run --name convention.adminpanel -d -p 8080:80 convention.adminpanel
docker run --name convention.website -d -p 8081:80 convention.website