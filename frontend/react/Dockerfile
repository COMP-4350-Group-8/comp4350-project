# Specify a base image
FROM node:22-alpine3.19

# Specify the working directory within the container
WORKDIR /usr/app

# Install some dependencies (after copying the package.json file into the container)
COPY ./package.json ./
RUN npm install

# Copy the remaining project files into the container
COPY ./ ./

# Default command
CMD ["npm", "run", "dev"]