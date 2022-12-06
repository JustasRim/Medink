export interface IEntity {
  name: string;
  lastName: string;
  id: number;
  email: string;
  number: string;
}

export interface IPatient extends IEntity {
  medicId: number;
}
