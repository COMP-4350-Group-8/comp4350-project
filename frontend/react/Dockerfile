# Specify a base image
FROM node:22-alpine

# Specify the working directory within the container
WORKDIR /usr/app

# Install some dependencies (after copying the package.json file into the container)
COPY ./package.json ./
RUN npm install

# Copy the remaining project files into the container
COPY ./ ./

# Make a build of the project
RUN npm run build

# Expose the port Vite will run on
EXPOSE 4173

# Default command
CMD ["npm", "run", "preview", "--", "--host", "0.0.0.0"]