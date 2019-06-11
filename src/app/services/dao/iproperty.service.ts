import { Property } from 'src/app/models/Property.model';

export interface IPropertyService {

    emitProperties(): void;
    saveProperties(): void;
    createProperty(newProperty: Property): void;
    removeProperty(property: Property): void;
    getProperties(): void;
    updateProperty(property: Property, id: number): void;
    uploadFile(file:File): Promise<any>;
    removePropertyPhoto(photoLink: string) : void;
    getSingleProperty(id: number) : Promise<Property>;

}